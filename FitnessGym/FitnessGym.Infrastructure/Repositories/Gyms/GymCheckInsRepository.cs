using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Domain.Entities.Identity;
using FitnessGym.Domain.Entities.Members;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Errors;
using FitnessGym.Infrastructure.Repositories.Interfaces.Gyms;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.Repositories.Gyms
{
    public class GymCheckInsRepository : GenericRepository<GymCheckIn>, IGymCheckInRepository
    {
        public GymCheckInsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Result<GymCheckIn>> GetActive(MembershipId membershipId)
        {
            var activeCheckIn = await _dbSet.AsNoTracking()
                                    .Where(checkIn => !checkIn.IsDeleted && checkIn.MembershipId == membershipId && checkIn.CheckOutTime == null)
                                    .FirstOrDefaultAsync();

            return activeCheckIn is not null ? Result.Ok(activeCheckIn) : Result.Fail(new NotFoundError(typeof(GymCheckIn)));
        }

        public async Task<Result<List<GymCheckIn>>> GetHistory(DateTime minimumDate, GymId gymId)
        {
            var checkInsHistory = await _dbSet.AsNoTracking()
                                    .Include(checkIn => checkIn.Membership)
                                    .ThenInclude(membership => membership.Member)
                                    .Where(checkIn => !checkIn.IsDeleted && checkIn.Membership.GymId == gymId && checkIn.CheckInTime >= minimumDate)
                                    .OrderByDescending(checkIn => checkIn.CheckInTime)
                                    .Select(checkIn => new GymCheckIn
                                    {
                                        Id = checkIn.Id,
                                        CheckInTime = checkIn.CheckInTime,
                                        CheckOutTime = checkIn.CheckOutTime,
                                        Membership = new Membership
                                        {
                                            Id = checkIn.MembershipId,
                                            MemberId = checkIn.Membership.MemberId,
                                            Member = new ApplicationUser
                                            {
                                                Id = checkIn.Membership.MemberId,
                                                Email = checkIn.Membership.Member.Email
                                            }
                                        }
                                    })
                                    .ToListAsync();

            return checkInsHistory is not null ? Result.Ok(checkInsHistory) : Result.Fail(new NotFoundError(typeof(GymCheckIn)));
        }

        public async Task<Result<int>> GetMembersInGym(GymId gymId)
        {
            var count = await _dbSet.AsNoTracking()
                .Include(checkIn => checkIn.Membership)
                .CountAsync(checkIn => !checkIn.IsDeleted && checkIn.Membership.GymId == gymId && checkIn.CheckOutTime == null);

            return Result.Ok(count);
        }
    }
}
