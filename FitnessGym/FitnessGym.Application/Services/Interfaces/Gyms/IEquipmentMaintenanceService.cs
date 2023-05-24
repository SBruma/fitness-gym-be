using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Application.Dtos.Gyms.Update;
using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Domain.Filters;
using FluentResults;

namespace FitnessGym.Application.Services.Interfaces.Gyms
{
    public interface IEquipmentMaintenanceService
    {
        Task<Result<EquipmentMaintenanceDto>> Create(CreateEquipmentMaintenanceDto maintenanceDto);
        Task<Result<EquipmentMaintenanceDto>> Update(MaintenanceHistoryId maintenanceId, UpdateEquipmentMaintenanceDto updateDto);
        Task<Result> Delete(MaintenanceHistoryId maintenanceHistoryId);
        Task<Result<EquipmentMaintenanceDto>> GetById(MaintenanceHistoryId maintenanceHistoryId);
        Task<Result<List<EquipmentMaintenanceDto>>> GetFiltered(EquipmentMaintenanceFilter maintenanceFilter, PaginationFilter paginationFilter);
    }
}
