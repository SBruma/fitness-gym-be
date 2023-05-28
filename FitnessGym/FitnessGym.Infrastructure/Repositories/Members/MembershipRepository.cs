using FitnessGym.Domain.Entities.Members;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Errors;
using FitnessGym.Infrastructure.Repositories.Interfaces.Members;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace FitnessGym.Infrastructure.Repositories.Members
{
    public class MembershipRepository : GenericRepository<Membership>, IMembershipRepository
    {
        public MembershipRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Result<Membership>> GetActiveMembership(Guid userId)
        {
            var membership = await _dbSet.AsNoTracking()
                                    .Where(membership => membership.MemberId == userId)
                                    .OrderByDescending(membership => membership.ExpirationDate)
                                    .FirstOrDefaultAsync();

            if (membership == null
                || DateTime.UtcNow > membership.ExpirationDate)
            {
                return Result.Fail(new NotFoundError(typeof(Membership)));
            }

            return Result.Ok(membership);
        }

        public async Task<Result<List<Membership>>> GetHistory(Guid userId)
        {
            var membershipHistory = await _dbSet.AsNoTracking()
                                                .Where(membership => membership.MemberId == userId)
                                                .OrderByDescending(membership => membership.ExpirationDate)
                                                .ToListAsync();

            return Result.Ok(membershipHistory);
        }
    }
}
