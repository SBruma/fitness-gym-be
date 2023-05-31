using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Application.Services.Interfaces.Gyms;
using FitnessGym.Domain.Entities.Gyms;
using Microsoft.AspNetCore.Mvc;

namespace FitnessGym.API.Controllers.Gyms
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase
    {
        private readonly IMembershipService _membershipService;

        public MembershipController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(MembershipDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Insert([FromBody] CreateMembershipDto request)
        {
            var result = await _membershipService.Create(request);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Reasons);
        }

        [HttpGet("last-active/")]
        [ProducesResponseType(typeof(List<MembershipDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetActive([FromQuery] Guid gymId, [FromQuery] string email)
        {
            var result = await _membershipService.GetActiveMembership(new GymId(gymId), email);

            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Reasons);
        }

        [HttpGet("history")]
        [ProducesResponseType(typeof(List<MembershipDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetHistory([FromQuery] Guid gymId, [FromQuery] string email)
        {
            var result = await _membershipService.GetHistory(new GymId(gymId), email);

            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Reasons);
        }
    }
}
