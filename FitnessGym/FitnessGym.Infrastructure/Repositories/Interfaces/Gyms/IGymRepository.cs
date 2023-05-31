using FitnessGym.Domain.Entities.Gyms;
using FluentResults;

namespace FitnessGym.Infrastructure.Repositories.Interfaces.Gyms
{
    public interface IGymRepository : IGenericRepository<Gym>
    {
        Task<Result<List<Gym>>> GetAll();
    }
}
