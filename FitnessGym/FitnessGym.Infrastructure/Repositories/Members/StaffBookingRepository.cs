using FitnessGym.Domain.Entities.Members;
using FitnessGym.Domain.Filters;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Repositories.Interfaces.Members;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.Repositories.Members
{
    public class StaffBookingRepository : GenericRepository<StaffBooking>, IStaffBookingRepository
    {
        public StaffBookingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Result<List<StaffBooking>>> GetStaffBookings(StaffBookingFilter staffBookingFilter)
        {
            var staffBookings = await Get(staffBookingFilter.GetQuery()).ToListAsync();

            return Result.Ok(staffBookings);
        }
    }
}
