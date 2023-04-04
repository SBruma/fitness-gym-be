using FitnessGym.Infrastructure.Data.Interfaces;
using FitnessGym.Infrastructure.Repositories.Interfaces.Gyms;
using FitnessGym.Infrastructure.Repositories.Interfaces.Members;

namespace FitnessGym.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IGymRepository _gymRepository;
        private readonly IFloorRepository _floorRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IMaintenanceHistoryRepository _maintenanceHistoryRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IStaffBookingRepository _staffBookingRepository;
        private readonly IStaffScheduleRepository _staffScheduleRepository;
        private readonly IMembershipRepository _membershipRepository;

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
            _gymRepository = gymRepository;
            _floorRepository = floorRepository;
            _equipmentRepository = equipmentRepository;
            _maintenanceHistoryRepository = maintenanceHistoryRepository;
            _memberRepository = memberRepository;
            _staffRepository = staffRepository;
            _staffBookingRepository = staffBookingRepository;
            _staffScheduleRepository = staffScheduleRepository;
            _membershipRepository = membershipRepository;
        }

        public async Task RollbackAsync()
        {
            await _context.DisposeAsync();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
