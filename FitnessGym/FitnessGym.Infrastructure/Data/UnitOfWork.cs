using FitnessGym.Infrastructure.Data.Interfaces;
using FitnessGym.Infrastructure.Repositories.Interfaces.Gyms;
using FitnessGym.Infrastructure.Repositories.Interfaces.Members;
using FluentResults;

namespace FitnessGym.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGymRepository GymRepository { get; }
        public IFloorRepository FloorRepository { get; }
        public IEquipmentRepository EquipmentRepository { get; }
        public IMaintenanceHistoryRepository MaintenanceHistoryRepository { get; }
        public IMemberRepository MemberRepository { get; }
        public IStaffRepository StaffRepository { get; }
        public IStaffBookingRepository StaffBookingRepository { get; }
        public IStaffScheduleRepository StaffScheduleRepository { get; }
        public IMembershipRepository MembershipRepository { get; }

        public UnitOfWork(ApplicationDbContext context,
                            IGymRepository gymRepository,
                            IFloorRepository floorRepository,
                            IEquipmentRepository equipmentRepository,
                            IMaintenanceHistoryRepository maintenanceHistoryRepository,
                            IMemberRepository memberRepository,
                            IStaffRepository staffRepository,
                            IStaffBookingRepository staffBookingRepository,
                            IStaffScheduleRepository staffScheduleRepository,
                            IMembershipRepository membershipRepository)
        {
            _context = context;
            GymRepository = gymRepository;
            FloorRepository = floorRepository;
            EquipmentRepository = equipmentRepository;
            MaintenanceHistoryRepository = maintenanceHistoryRepository;
            MemberRepository = memberRepository;
            StaffRepository = staffRepository;
            StaffBookingRepository = staffBookingRepository;
            StaffScheduleRepository = staffScheduleRepository;
            MembershipRepository = membershipRepository;
        }

        public async Task RollbackAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<Result> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail(new Error(e.Message + e.InnerException));
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
