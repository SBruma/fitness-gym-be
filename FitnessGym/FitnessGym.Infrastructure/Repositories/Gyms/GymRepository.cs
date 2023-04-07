using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Repositories.Interfaces.Gyms;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.Repositories.Gyms
{
    public class GymRepository : GenericRepository<Gym>, IGymRepository
    {
        public GymRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Gym>> GetAll()
        {
            return await _dbSet.Where(gym => gym.IsDeleted == false).AsNoTracking().ToListAsync();
        }
    }
}
