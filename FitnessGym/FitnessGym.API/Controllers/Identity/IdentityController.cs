using FitnessGym.Application.Dtos.Identity;
using FitnessGym.Application.Services.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitnessGym.API.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
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
    }
}
