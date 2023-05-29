using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Domain.Entities.Members;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Errors;
using FitnessGym.Infrastructure.Repositories.Interfaces.Members;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.Repositories.Members
{
    public class MembershipRepository : GenericRepository<Membership>, IMembershipRepository
    {
        public MembershipRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Result<Membership>> GetActiveMembership(GymId gymId, Guid userId)
        {
            var membership = await _dbSet.AsNoTracking()
                                    .Where(membership => membership.GymId == gymId
                                                        && membership.MemberId == userId)
                                    .OrderByDescending(membership => membership.ExpirationDate)
                                    .FirstOrDefaultAsync();

            if (membership == null
                || DateTime.UtcNow > membership.ExpirationDate)
            {
                return Result.Fail(new NotFoundError(typeof(Membership)));
            }

            return Result.Ok(membership);
        }

        public async Task<Result<List<Membership>>> GetHistory(GymId gymId, Guid userId)
        {
            var membershipHistory = await _dbSet.AsNoTracking()
                                                .Where(membership => membership.GymId == gymId
                                                                    && membership.MemberId == userId)
                                                .OrderByDescending(membership => membership.ExpirationDate)
                                                .ToListAsync();

            return Result.Ok(membershipHistory);
        }
    }
}
