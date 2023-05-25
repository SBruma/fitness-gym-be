using FitnessGym.Application.Dtos.Identity;
using FitnessGym.Application.Options;
using FitnessGym.Application.Services.Interfaces.Identity;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FitnessGym.API.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly AppOptions _appOptions;

        public IdentityController(IIdentityService identityService, IOptionsSnapshot<AppOptions> appOptions)
        {
            _identityService = identityService;
            _appOptions = appOptions.Value;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateAccount(RegisterDto registerDto)
        {
            var result = await _identityService.Register(registerDto);

            return result.IsSuccess ? Ok() : BadRequest();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _identityService.Login(loginDto);

            return result.IsSuccess ? Ok(result.Value) : BadRequest();
        }

        [HttpGet]
        [Route("signin-google")]
        public async Task<IActionResult> GoogleCallback(string code, CancellationToken cancellationToken)
        {
            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = _appOptions.ClientId,
                    ClientSecret = _appOptions.ClientSecret
                }
            });

            var googleCallbackUrl = Url.Action(nameof(GoogleCallback), null, null, Request.Scheme, Request.Host.Value);
            var tokenResponse = await flow.ExchangeCodeForTokenAsync("holder", code, googleCallbackUrl, cancellationToken);

            return Ok();
        }

        [HttpGet("google-redirect")]
        public IActionResult RedirectToGoogleAuth()
        {
            var googleCallbackUrl = Url.Action(nameof(GoogleCallback), null, null, Request.Scheme, Request.Host.Value);

            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
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
                    "openid",
                }
            });

            var authUrl = flow.CreateAuthorizationCodeRequest(googleCallbackUrl).Build().AbsoluteUri;

            return Redirect(authUrl);
        }

    }
}
