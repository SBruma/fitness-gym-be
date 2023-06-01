using FitnessGym.Application.Dtos.Identity;
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
        private readonly RoleManager<ApplicationUser> _roleManager;
        private readonly IMapper _mapper;
        private readonly AppOptions _appOptions;

        public IdentityService(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                IMapper mapper,
                                IOptionsSnapshot<AppOptions> appOptions,
                                RoleManager<ApplicationUser> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _appOptions = appOptions.Value;
            _roleManager = roleManager;
        }

        public async Task<Result<TokenData>> Login(LoginDto loginDto)
        {
            var loginResult = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);

            if (!loginResult.Succeeded)
            {
                return Result.Fail("Incorrect credentials");
            }

            var user = await _userManager.FindByNameAsync(loginDto.Email);
            var tokenResponse = await GenerateTokenAsync(user);
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

        public async Task<TokenData> GenerateTokenAsync(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
                    new Claim(JwtClaimTypes.Name, user.UserName),
                    new Claim(JwtClaimTypes.Audience, _appOptions.Audience),
                    new Claim(JwtClaimTypes.Issuer, _appOptions.Issuer)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appOptions.Key)), SecurityAlgorithms.HmacSha256Signature)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in )
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
    }
}
