using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Repositories.Interfaces.Gyms;

namespace FitnessGym.Infrastructure.Repositories.Gyms
{
    public class GymRepository : GenericRepository<Gym>, IGymRepository
    {
        public GymRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
