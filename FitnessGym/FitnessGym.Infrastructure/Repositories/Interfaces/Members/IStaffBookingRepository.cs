using FitnessGym.Domain.Entities.Members;
using FitnessGym.Domain.Filters;
using FluentResults;

namespace FitnessGym.Infrastructure.Repositories.Interfaces.Members
{
    public interface IStaffBookingRepository : IGenericRepository<StaffBooking>
    {
        Task<Result<List<StaffBooking>>> GetStaffBookings(StaffBookingFilter staffBookingFilter);
    }
}
