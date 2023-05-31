using FitnessGym.Infrastructure.Repositories.Interfaces.Gyms;
using FitnessGym.Infrastructure.Repositories.Interfaces.Members;
using FluentResults;

namespace FitnessGym.Infrastructure.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGymRepository GymRepository { get; }
        IFloorRepository FloorRepository { get; }
        IEquipmentRepository EquipmentRepository { get; }
        IMaintenanceHistoryRepository MaintenanceHistoryRepository { get; }
        IMemberRepository MemberRepository { get; }
        IStaffRepository StaffRepository { get; }
        IStaffBookingRepository StaffBookingRepository { get; }
        IStaffScheduleRepository StaffScheduleRepository { get; }
        IMembershipRepository MembershipRepository { get; }
        Task<Result> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync();
    }
}
