using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Repositories.Interfaces.Gyms;

namespace FitnessGym.Infrastructure.Repositories.Gyms
{
    public class FloorRepository : GenericRepository<Floor>, IFloorRepository
    {
        public FloorRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
