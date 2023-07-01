using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Update;
using FitnessGym.Application.Services.Interfaces.Gyms;
using Microsoft.AspNetCore.Mvc;

namespace FitnessGym.API.Controllers.Gyms
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffScheduleController : ControllerBase
    {
        private readonly IStaffScheduleService _staffScheduleService;

        public StaffScheduleController(IStaffScheduleService staffScheduleService)
        {
            _staffScheduleService = staffScheduleService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(StaffScheduleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Insert([FromBody] StaffScheduleDto schedule)
        {
            var result = await _staffScheduleService.CreateSchedule(schedule);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Reasons);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StaffScheduleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(Guid staffId)
        {
            var result = await _staffScheduleService.GetStaffSchedule(staffId);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid staffId, [FromBody] UpdateStaffSchedule updateStaffSchedule)
        {
            var result = await _staffScheduleService.UpdateSchedule(staffId, updateStaffSchedule);

            return result.IsSuccess ? Ok() : NotFound();
        }
    }
}
