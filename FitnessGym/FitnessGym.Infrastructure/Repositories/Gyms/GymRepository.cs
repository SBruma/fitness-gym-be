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
            return await _dbSet.AsNoTracking()
                                .Where(gym => gym.IsDeleted == false)
                                .ToListAsync();
        }

        public override async Task<Gym?> GetById(object entityId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking()
                                .Include(gym => gym.Floors.Where(floor => floor.IsDeleted == false))
                                .Where(gym => gym.Id == (GymId)entityId)
                                .FirstOrDefaultAsync();
        }
    }
}
