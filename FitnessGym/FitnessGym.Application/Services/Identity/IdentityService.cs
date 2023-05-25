using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.ResponseHandling;
using FitnessGym.Application.Dtos.Identity;
using FitnessGym.Application.Mappers;
using FitnessGym.Application.Options;
using FitnessGym.Application.Services.Interfaces.Identity;
using FitnessGym.Domain.Entities.Identity;
using FluentResults;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
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

        public async Task<Result<TokenResponse>> Login(LoginDto loginDto)
        {
            var loginResult = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);

            if (!loginResult.Succeeded)
            {
                return Result.Fail("Incorrect credentials");
            }

            var user = await _userManager.FindByNameAsync(loginDto.Email);
            var tokenResponse = GenerateTokenAsync(user);


            return Result.Ok(tokenResponse);
        }

        public async Task<Result> Register(RegisterDto registerDto)
        {
            var userAccount = _mapper.IdentityMapper.RegisterDtoToUser(registerDto);
            var registerResult = await _userManager.CreateAsync(userAccount, registerDto.Password);

            return Result.OkIf(registerResult.Succeeded, new Error("problema"));
        }

        private TokenResponse GenerateTokenAsync(ApplicationUser user)
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

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            var refreshToken = Guid.NewGuid().ToString();

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        //public async Task<Result<TokenResponse>> LoginGoogle(GoogleLoginDto googleLoginDto)
        //{


        //    //// Validate the access token
        //    //var tokenValidationParameters = new TokenValidationParameters
        //    //{
        //    //    ValidateIssuer = false, // Customize as needed
        //    //    ValidateAudience = false, // Customize as needed
        //    //    ValidateLifetime = true,
        //    //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YOUR_SECRET_KEY"))
        //    //};

        //    //var jwtHandler = new JwtSecurityTokenHandler();
        //    //var claimsPrincipal = jwtHandler.ValidateToken(
        //    //    tokenResponse.AccessToken,
        //    //    tokenValidationParameters,
        //    //    out _);

        //    //// Retrieve user information from the claims
        //    //var emailClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
        //    //var nameClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

        //    //// Perform additional checks or operations with the user information
        //    //// For example, check if the user exists in your system or create a new user account

        //    //// Return a response or generate a JWT token as needed

        //    return Result.Ok(tokenResponse);
        //}
    }
}
