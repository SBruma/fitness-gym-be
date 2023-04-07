using FitnessGym.Domain.Entities.Gyms;

namespace FitnessGym.Infrastructure.Repositories.Interfaces.Gyms
{
    public interface IGymRepository : IGenericRepository<Gym>
    {
        Task<List<Gym>> GetAll();
    }
}
