using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Errors;
using FitnessGym.Infrastructure.Repositories.Interfaces.Gyms;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.Repositories.Gyms
{
    public class GymRepository : GenericRepository<Gym>, IGymRepository
    {
        public GymRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Result<List<Gym>>> GetAll()
        {
            var gyms = await _dbSet.AsNoTracking()
                                .Where(gym => gym.IsDeleted == false)
                                .ToListAsync();

            return gyms.Count > 0 ? Result.Ok(gyms) : Result.Fail(new NotFoundError(typeof(List<Gym>)));
        }

        public override async Task<Result<Gym>> GetById(object entityId, CancellationToken cancellationToken = default)
        {
            var gym = await _dbSet.AsNoTracking()
                                    .Include(gym => gym.Floors.Where(floor => floor.IsDeleted == false))
                                    .Where(gym => gym.Id == (GymId)entityId)
                                    .FirstOrDefaultAsync();

            return gym is not null ? Result.Ok(gym) : Result.Fail(new NotFoundError(typeof(Gym)));
        }
    }
}
