using FitnessGym.Domain.Entities;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Repositories.Interfaces.Members;

namespace FitnessGym.Infrastructure.Repositories.Members
{
    public class StaffScheduleRepository : GenericRepository<StaffSchedule>, IStaffScheduleRepository
    {
        public StaffScheduleRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
