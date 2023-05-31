using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Domain.Filters;
using FluentResults;

namespace FitnessGym.Infrastructure.Repositories.Interfaces.Gyms
{
    public interface IMaintenanceHistoryRepository : IGenericRepository<MaintenanceHistory>
    {
        Task<Result<MaintenanceHistory>> GetActiveMaintenance(EquipmentId equipmentId);
        Task<Result<List<MaintenanceHistory>>> GetFiltered(EquipmentMaintenanceFilter maintenanceFilter, PaginationFilter paginationFilter);
    }
}
