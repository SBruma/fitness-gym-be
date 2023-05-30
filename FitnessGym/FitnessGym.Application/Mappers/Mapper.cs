using FitnessGym.Application.Mappers.Gyms;
using FitnessGym.Application.Mappers.Identity;

namespace FitnessGym.Application.Mappers
{
    public class Mapper : IMapper
    {
        public FloorMapper FloorMapper { get; }
        public GymMapper GymMapper { get; }
        public EquipmentMapper EquipmentMapper { get; }
        public IdentityMapper IdentityMapper { get; }
        public EquipmentMaintenanceMapper EquipmentMaintenanceMapper { get; }
        public MembershipMapper MembershipMapper { get; }
        public StaffScheduleMapper StaffScheduleMapper { get; }
        public StaffBookingMapper StaffBookingMapper { get; }

        public Mapper()
        {
            FloorMapper = new FloorMapper();
            GymMapper = new GymMapper(FloorMapper);
            EquipmentMapper = new EquipmentMapper();
            IdentityMapper = new IdentityMapper();
            EquipmentMaintenanceMapper = new EquipmentMaintenanceMapper();
            MembershipMapper = new MembershipMapper();
            StaffScheduleMapper = new StaffScheduleMapper();
            StaffBookingMapper = new StaffBookingMapper();
        }
    }
}
