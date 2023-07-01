using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Domain.Filters;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Repositories.Interfaces.Gyms;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.Repositories.Gyms
{
    public class EquipmentRepository : GenericRepository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Equipment>> GetFiltered(EquipmentFilter equipmentFilter, PaginationFilter paginationFilter)
        {
            return await Get(equipmentFilter.GetQuery(), paginationFilter).ToListAsync();
        }

        public async Task<bool> ValidateWarrantyExpirationJob()
        {
            var dateNow = DateTime.UtcNow;
            var expiredWarrantiesEquipments = await _dbSet.Where(e => !e.IsDeleted
                                                                    && new DateOnly(dateNow.Year, dateNow.Month, dateNow.Day) >= e.WarrantyExpirationDate)
                                                            .ToListAsync();

            foreach (var equipment in expiredWarrantiesEquipments)
            {
                equipment.Status = Domain.Entities.Enums.EquipmentStatus.NeedMaintenance;
            }

            _dbSet.UpdateRange(expiredWarrantiesEquipments);

            return expiredWarrantiesEquipments.Any();
        }
    }
}
