using FitnessGym.Domain.Entities.Members;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Repositories.Interfaces.Members;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.Repositories.Members
{
    public class StaffScheduleRepository : GenericRepository<StaffSchedule>, IStaffScheduleRepository
    {
        public StaffScheduleRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<Result<List<StaffSchedule>>> GetStaffSchedule(Guid staffId)
        {
            var schedules = await _dbSet.AsNoTracking()
                                        .Where(schedule => schedule.StaffId == staffId)
                                        .ToListAsync();

            return Result.Ok(schedules);
        }
    }
}
