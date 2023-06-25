using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Application.Dtos.Gyms.Update;
using FitnessGym.Application.Dtos.Identity;
using FitnessGym.Application.Errors;
using FitnessGym.Application.Mappers;
using FitnessGym.Application.Options;
using FitnessGym.Application.Services.Interfaces.Identity;
using FitnessGym.Domain.Entities.Identity;
using FitnessGym.Domain.Entities.Statics;
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
            user.AccesToken = tokenResponse.AccessToken;///verify refresh token
            user.RefreshToken = tokenResponse.RefreshToken;
            await _userManager.UpdateAsync(user);

            return Result.Ok(tokenResponse);
        }

        public async Task<Result<ApplicationUser>> Register(RegisterDto registerDto)
        {
            var userAccount = _mapper.IdentityMapper.RegisterDtoToUser(registerDto);
            userAccount.UserName = userAccount.Email;
            var registerResult = await _userManager.CreateAsync(userAccount, registerDto.Password);
            var roleRegisterResult = await _userManager.AddToRoleAsync(userAccount, Roles.Member);

            return registerResult.Succeeded && roleRegisterResult.Succeeded ?
                Result.Ok(userAccount) : Result.Fail(new Error("Register failed"));
        }

        public async Task<Result<ApplicationUser>> Add(AddMemberDto addMemberDto)
        {
            var userAccount = _mapper.IdentityMapper.RegisterDtoToUser(addMemberDto);
            userAccount.UserName = userAccount.Email;
            var registerResult = await _userManager.CreateAsync(userAccount, $"_G{addMemberDto.Email}14");

            if (!registerResult.Succeeded)
            {
                return Result.Fail(new Error("User couldn't be created"));
            }

            var user = await _userManager.FindByEmailAsync(userAccount.Email);

            foreach (var role in addMemberDto.Roles)
            {
                var roleRegisterResult = await _userManager.AddToRoleAsync(userAccount, role);

                if (!roleRegisterResult.Succeeded)
                {
                    await _userManager.DeleteAsync(user);
                    return Result.Fail(new Error("Roles couldn't be added"));
                }
            }

            return Result.Ok(user);
        }

        public async Task<Result<TokenData>> Update(UpdateUserDto updateUserDto, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            _mapper.IdentityMapper.Update(updateUserDto, user);
            var newTokenData = await GenerateToken(user);
            user.RefreshToken = newTokenData.RefreshToken;
            user.AccesToken = newTokenData.AccessToken;
            var updateResult = await _userManager.UpdateAsync(user);

            return updateResult.Succeeded ? Result.Ok(newTokenData) : Result.Fail(new UpdateError(user.Id));
        }

        public async Task<Result> UpdatePassword(UpdatePasswordDto updatePasswordDto, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.ChangePasswordAsync(user, updatePasswordDto.CurrentPassword, updatePasswordDto.NewPassword);

            return result.Succeeded ? Result.Ok() : Result.Fail(new Error(result.Errors.First().Description));
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
                    new Claim(JwtClaimTypes.Issuer, _appOptions.Issuer),
                    new Claim(JwtClaimTypes.Picture, user.ProfilePicture),
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

            var newToken = await GenerateToken(user);
            user.AccesToken = newToken.AccessToken;
            newToken.RefreshToken = user.RefreshToken;
            await _userManager.UpdateAsync(user);

            return Result.Ok(newToken);
        }
    }
}
