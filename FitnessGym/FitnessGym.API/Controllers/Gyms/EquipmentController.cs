using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Application.Services.Gyms;
using FitnessGym.Application.Services.Interfaces.Gyms;
using FitnessGym.Domain.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FitnessGym.API.Controllers.Gyms
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(EquipmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Insert([FromBody] CreateEquipmentDto request)
        {
            var result = await _equipmentService.Create(request);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<EquipmentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get([FromQuery] EquipmentFilter equipmentFilter, [FromQuery] PaginationFilter paginationFilter)
        {
            var result = await _equipmentService.Get(equipmentFilter, paginationFilter);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }
    }
}
