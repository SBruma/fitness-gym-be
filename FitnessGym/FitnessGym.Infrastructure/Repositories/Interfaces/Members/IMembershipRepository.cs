using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Domain.Entities.Members;
using FluentResults;

namespace FitnessGym.Infrastructure.Repositories.Interfaces.Members
{
    public interface IMembershipRepository : IGenericRepository<Membership>
    {
        Task<Result<Membership>> GetActiveMembership(GymId gymId, Guid userId);
        Task<Result<List<Membership>>> GetHistory(GymId gymId, Guid userId);
    }
}
