using FitnessGym.Domain.Entities;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Repositories.Interfaces.Members;

namespace FitnessGym.Infrastructure.Repositories.Members
{
    public class StaffBookingRepository : GenericRepository<StaffBooking>, IStaffBookingRepository
    {
        public StaffBookingRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
