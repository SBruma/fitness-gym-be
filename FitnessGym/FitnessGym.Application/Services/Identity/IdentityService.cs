using FitnessGym.Application.Dtos.Identity;
using FitnessGym.Application.Errors;
using FitnessGym.Application.Mappers;
using FitnessGym.Application.Options;
using FitnessGym.Application.Services.Interfaces.Identity;
using FitnessGym.Domain.Entities.Identity;
using FluentResults;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FitnessGym.Application.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly AppOptions _appOptions;

        public IdentityService(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                IMapper mapper,
                                IOptionsSnapshot<AppOptions> appOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _appOptions = appOptions.Value;
        }

        public async Task<Result<TokenData>> Login(LoginDto loginDto)
        {
            var loginResult = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);

            if (!loginResult.Succeeded)
            {
                return Result.Fail("Incorrect credentials");
            }

            var user = await _userManager.FindByNameAsync(loginDto.Email);
            var tokenResponse = await GenerateToken(user);
            user.AccesToken = tokenResponse.AccessToken;
            user.RefreshToken = tokenResponse.RefreshToken;
            await _userManager.UpdateAsync(user);

            return Result.Ok(tokenResponse);
        }

        public async Task<Result<ApplicationUser>> Register(RegisterDto registerDto)
        {
            var userAccount = _mapper.IdentityMapper.RegisterDtoToUser(registerDto);
            userAccount.UserName = userAccount.Email;
            var registerResult = await _userManager.CreateAsync(userAccount, registerDto.Password);

            return registerResult.Succeeded ? Result.Ok(userAccount) : Result.Fail(new Error("Register failed"));
        }

        public async Task<TokenData> GenerateToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
                    new Claim(JwtClaimTypes.Name, $"{user.LastName} {user.FirstName}"),
                    new Claim(JwtClaimTypes.FamilyName, user.LastName),
                    new Claim(JwtClaimTypes.GivenName, user.FirstName),
                    new Claim(JwtClaimTypes.Email, user.Email),
                    new Claim(JwtClaimTypes.BirthDate, user.DateOfBirth.ToString()),
                    new Claim(JwtClaimTypes.Gender, user.Gender.ToString()),
                    new Claim(JwtClaimTypes.Audience, _appOptions.Audience),
                    new Claim(JwtClaimTypes.Issuer, _appOptions.Issuer)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appOptions.Key)), SecurityAlgorithms.HmacSha256Signature)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(JwtClaimTypes.Role, role));
            }

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            var refreshToken = Guid.NewGuid().ToString();

            return new TokenData
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<Result<TokenData>> RefreshToken(TokenData tokenData)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(tokenData.AccessToken);
            var userEmail = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user is null)
            {
                return Result.Fail(new NotFoundError(typeof(ApplicationUser)));
            }

            if (user.RefreshToken != tokenData.RefreshToken)
            {
                return Result.Fail(new Error("Refresh token is invalid"));
            }

            return await GenerateToken(user);
        }
    }
}
