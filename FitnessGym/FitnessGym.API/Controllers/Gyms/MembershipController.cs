using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Application.Services.Interfaces.Gyms;
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
        [ProducesResponseType(typeof(EquipmentMaintenanceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Insert([FromBody] CreateMembershipDto request)
        {
            var result = await _membershipService.Create(request);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Reasons);
        }

        [HttpGet("last-active/{email}")]
        [ProducesResponseType(typeof(List<EquipmentMaintenanceDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetActive(string email)
        {
            var result = await _membershipService.GetActiveMembership(email);

            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Reasons);
        }

        [HttpGet("history/{email}")]
        [ProducesResponseType(typeof(List<EquipmentMaintenanceDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetHistory(string email)
        {
            var result = await _membershipService.GetHistory(email);

            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Reasons);
        }
    }
}
