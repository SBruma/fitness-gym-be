using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Update;
using FitnessGym.Application.Dtos.Identity;
using FitnessGym.Application.Options;
using FitnessGym.Application.Services.Interfaces.Identity;
using FitnessGym.Application.Services.Interfaces.Others;
using FitnessGym.Domain.Entities.Identity;
using FluentResults;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace FitnessGym.API.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IEmailService _emailService;
        private readonly AppOptions _appOptions;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityController(IIdentityService identityService,
                                    IOptionsSnapshot<AppOptions> appOptions,
                                    UserManager<ApplicationUser> userManager,
                                    IEmailService emailService)
        {
            _identityService = identityService;
            _appOptions = appOptions.Value;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateAccount(RegisterDto registerDto)
        {
            var registerResult = await _identityService.Register(registerDto);

            if (registerResult.IsFailed)
            {
                return BadRequest("Email already registered");
            }

            string acceptLanguage = Request.Headers["Accept-Language"];
            var user = registerResult.Value;
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"{_appOptions.BaseURL}/{acceptLanguage}/pages/confirm-email?email={user.Email}&token={token}";
            var conf = UriHelper.Encode(new Uri(confirmationLink));

            Result sendEmailResult;

            if (acceptLanguage == "ro")
            {
                sendEmailResult = _emailService.SendEmail(new MailData
                {
                    EmailSubject = "Confirma emailul",
                    EmailBody = $"<h2>Confirma emailul</h2><p>Draga {user.LastName} {user.FirstName},</p>" +
                               $"<p>Multumim pentru inregistrare. Vă rugăm să faceți clic pe următorul link pentru a vă confirma e-mailul:</p>" +
                               $"<p><a href=\"{confirmationLink}\">Click aici pentru a confirma</a></p>",
                    EmailToId = user.Email,
                    EmailToName = $"{user.LastName} {user.FirstName}"
                });
            }
            else
            {
                sendEmailResult = _emailService.SendEmail(new MailData
                {
                    EmailSubject = "Confirm your email",
                    EmailBody = $"<h2>Confirm your email</h2><p>Dear {user.LastName} {user.FirstName},</p>" +
                               $"<p>Thank you for registering. Please click the following link to confirm your email:</p>" +
                               $"<p><a href=\"{confirmationLink}\">Click here to confirm</a></p>",
                    EmailToId = user.Email,
                    EmailToName = $"{user.LastName} {user.FirstName}"
                });
            }

            if (sendEmailResult.IsFailed)
            {
                await _userManager.DeleteAsync(user);
            }

            return sendEmailResult.IsSuccess ? Ok() : BadRequest(sendEmailResult.Reasons);
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto confirmEmailDto)
        {
            var user = await _userManager.FindByEmailAsync(confirmEmailDto.Email);

            if (user is null)
            {
                return BadRequest();
            }

            var cofirmEmailResult = await _userManager.ConfirmEmailAsync(user, confirmEmailDto.Token);

            return cofirmEmailResult.Succeeded ? Ok() : BadRequest();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _identityService.Login(loginDto);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Reasons);
        }

        [HttpGet("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromQuery] string refreshToken, [FromQuery] string accessToken)
        {
            var newTokenResult = await _identityService.RefreshToken(new TokenData { AccessToken = accessToken, RefreshToken = refreshToken });

            return newTokenResult.IsSuccess ? Ok(newTokenResult.Value) : BadRequest();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(UpdateUserDto dto)
        {
            var result = await _identityService.Update(dto, GetEmailFromToken());

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Reasons);
        }

        [HttpPut("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(UpdatePasswordDto updatePasswordDto)
        {
            var result = await _identityService.UpdatePassword(updatePasswordDto, GetEmailFromToken());

            return result.IsSuccess ? Ok(result) : BadRequest(result.Reasons);
        }

        [HttpGet]
        [Route("signin-google")]
        public async Task<IActionResult> GoogleCallback(string code, CancellationToken cancellationToken)
        {
            var flow = GetGoogleFlow();
            var googleCallbackUrl = Url.Action(nameof(GoogleCallback), null, null, Request.Scheme, Request.Host.Value);
            var tokenResponse = await flow.ExchangeCodeForTokenAsync("", code, googleCallbackUrl, cancellationToken);
            var googleToken = new GoogleTokenDto
            {
                AccessToken = tokenResponse.AccessToken,
                JwtToken = tokenResponse.IdToken,
                RefreshToken = tokenResponse.RefreshToken
            };

            var userInfo = await GetGoogleUserInfo(googleToken.AccessToken);
            var applicationUser = await _userManager.FindByEmailAsync(userInfo.Email);

            if (applicationUser is null)
            {
                applicationUser = new ApplicationUser
                {
                    LastName = userInfo.Family_Name,
                    FirstName = userInfo.Given_Name,
                    Email = userInfo.Email,
                    EmailConfirmed = userInfo.Email_Verified,
                    UserName = userInfo.Email,
                    DateOfBirth = new DateOnly()
                };
                var createUser = await _userManager.CreateAsync(applicationUser);

                if (!createUser.Succeeded)
                {
                    return BadRequest();
                }
            }

            await _userManager.SetAuthenticationTokenAsync(applicationUser, "Google", "access_token", googleToken.JwtToken);
            //await _userManager.SetAuthenticationTokenAsync(applicationUser, "Google", "refresh_token", googleToken.RefreshToken);

            return Ok(_identityService.GenerateToken(applicationUser));
        }

        [HttpGet]
        [Route("google-refresh-token")]
        public async Task<IActionResult> RefreshAccessToken(string refreshToken, CancellationToken cancellationToken)
        {
            var flow = GetGoogleFlow();
            var tokenResponse = await flow.RefreshTokenAsync("", refreshToken, cancellationToken);
            var googleToken = new GoogleTokenDto
            {
                AccessToken = tokenResponse.AccessToken,
                JwtToken = tokenResponse.IdToken,
                RefreshToken = tokenResponse.RefreshToken
            };

            return Ok(googleToken);
        }

        [HttpGet("google-redirect")]
        public IActionResult RedirectToGoogleAuth()
        {
            var googleCallbackUrl = Url.Action(nameof(GoogleCallback), null, null, Request.Scheme, Request.Host.Value);
            var flow = GetGoogleFlow();
            var authUrl = flow.CreateAuthorizationCodeRequest(googleCallbackUrl).Build().AbsoluteUri;

            return Redirect(authUrl);
        }

        private GoogleAuthorizationCodeFlow GetGoogleFlow()
        {
            return new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = _appOptions.ClientId,
                    ClientSecret = _appOptions.ClientSecret
                },
                Scopes = new List<string>
                {
                    "https://www.googleapis.com/auth/userinfo.email",
                    "https://www.googleapis.com/auth/userinfo.profile",
                    "openid"
                }
            });
        }

        private async Task<GoogleUserInfo> GetGoogleUserInfo(string accessToken)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.GetAsync("https://www.googleapis.com/oauth2/v3/userinfo");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var userInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<GoogleUserInfo>(content);

                return userInfo;
            }

            // Handle error case if the request fails
            throw new Exception("Failed to retrieve user information from Google.");
        }

        private string GetEmailFromToken()
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            return jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
        }
    }
}
