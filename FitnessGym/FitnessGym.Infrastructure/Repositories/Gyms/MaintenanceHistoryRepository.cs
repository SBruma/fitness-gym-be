using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Domain.Filters;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Errors;
using FitnessGym.Infrastructure.Repositories.Interfaces.Gyms;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.Repositories.Gyms
{
    public class MaintenanceHistoryRepository : GenericRepository<MaintenanceHistory>, IMaintenanceHistoryRepository
    {
        public MaintenanceHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Result<MaintenanceHistory>> GetActiveMaintenance(EquipmentId equipmentId)
        {
            var maintenanceHistory = await _dbSet.Where(maintenanceHistory => maintenanceHistory.IsDeleted == false
                                                                            && maintenanceHistory.EndDate == null
                                                                            && maintenanceHistory.EquipmentId == equipmentId)
                                                .FirstOrDefaultAsync();

            return maintenanceHistory is not null ? Result.Ok(maintenanceHistory) :
                                                    Result.Fail(new NotFoundError(typeof(MaintenanceHistory)));
        }

        public async Task<Result<List<MaintenanceHistory>>> GetFiltered(EquipmentMaintenanceFilter maintenanceFilter, PaginationFilter paginationFilter)
        {
            var maintenanceHistory = await Get(maintenanceFilter.GetFilterQuery(), paginationFilter).ToListAsync();

            return Result.Ok(maintenanceHistory);
        }
    }
}
