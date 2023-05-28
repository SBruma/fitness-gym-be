using FitnessGym.Domain.Entities.Members;
using FluentResults;

namespace FitnessGym.Infrastructure.Repositories.Interfaces.Members
{
    public interface IMembershipRepository : IGenericRepository<Membership>
    {
        Task<Result<Membership>> GetActiveMembership(Guid userId);
        Task<Result<List<Membership>>> GetHistory(Guid userId);
    }
}
