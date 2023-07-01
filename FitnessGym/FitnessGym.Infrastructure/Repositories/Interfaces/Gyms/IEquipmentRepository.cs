using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Domain.Filters;

namespace FitnessGym.Infrastructure.Repositories.Interfaces.Gyms
{
    public interface IEquipmentRepository : IGenericRepository<Equipment>
    {
        Task<List<Equipment>> GetFiltered(EquipmentFilter equipmentFilter, PaginationFilter paginationFilter);
        Task<bool> ValidateWarrantyExpirationJob();
    }
}
