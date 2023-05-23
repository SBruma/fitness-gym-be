using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Application.Dtos.Gyms.Expanded;
using FitnessGym.Application.Dtos.Gyms.Update;
using FitnessGym.Application.Services.Gyms;
using FitnessGym.Application.Services.Interfaces.Gyms;
using FitnessGym.Domain.Entities.Gyms;
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

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Reasons);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<EquipmentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get([FromQuery] EquipmentFilter equipmentFilter, [FromQuery] PaginationFilter paginationFilter)
        {
            var result = await _equipmentService.Get(equipmentFilter, paginationFilter);

            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Reasons);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<ExpandedEquipmentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _equipmentService.GetById(new EquipmentId(id));

            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Reasons);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _equipmentService.Delete(new EquipmentId(id));

            return result.IsSuccess ? Ok() : NotFound(result.Reasons);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(List<ExpandedEquipmentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEquipmentDto updateEquipmentDto)
        {
            var result = await _equipmentService.Update(new EquipmentId(id), updateEquipmentDto);

            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Reasons);
        }
    }
}
