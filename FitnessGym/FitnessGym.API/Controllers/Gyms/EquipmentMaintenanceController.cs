using FitnessGym.API.Headers;
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
    public class EquipmentMaintenanceController : ControllerBase
    {
        private readonly IEquipmentMaintenanceService _maintenanceService;

        public EquipmentMaintenanceController(IEquipmentMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(EquipmentMaintenanceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Insert([FromBody] CreateEquipmentMaintenanceDto request)
        {
            var result = await _maintenanceService.Create(request);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Reasons);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<EquipmentMaintenanceDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get([FromQuery] EquipmentMaintenanceFilter maintenanceFilter, [FromQuery] PaginationQueryFilter paginationFilter)
        {
            var result = await _maintenanceService.GetFiltered(maintenanceFilter, new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize));

            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Reasons);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<EquipmentMaintenanceDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _maintenanceService.GetById(new MaintenanceHistoryId(id));

            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Reasons);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _maintenanceService.Delete(new MaintenanceHistoryId(id));

            return result.IsSuccess ? Ok() : NotFound(result.Reasons);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(List<EquipmentMaintenanceDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEquipmentMaintenanceDto updateDto)
        {
            var result = await _maintenanceService.Update(new MaintenanceHistoryId(id), updateDto);

            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Reasons);
        }
    }
}
