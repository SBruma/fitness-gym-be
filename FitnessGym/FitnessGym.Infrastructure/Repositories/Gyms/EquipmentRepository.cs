using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Repositories.Interfaces.Gyms;

namespace FitnessGym.Infrastructure.Repositories.Gyms
{
    public class EquipmentRepository : GenericRepository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
