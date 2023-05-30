using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Application.Services.Interfaces.Gyms;
using FitnessGym.Domain.Entities.Members;
using FitnessGym.Domain.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FitnessGym.API.Controllers.Gyms
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffBookingController : ControllerBase
    {
        private readonly IStaffBookingService _staffBookingService;

        public StaffBookingController(IStaffBookingService staffBookingService)
        {
            _staffBookingService = staffBookingService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(StaffBookingDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Insert([FromBody] CreateStaffBookingDto request)
        {
            var result = await _staffBookingService.Create(request);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StaffBookingDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _staffBookingService.GetById(new StaffBookingId(id));

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<StaffBookingDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] StaffBookingFilter staffBookingFilter)
        {
            var result = await _staffBookingService.GetFitlered(staffBookingFilter);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }
    }
}
