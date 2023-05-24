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

        public Mapper()
        {
            FloorMapper = new FloorMapper();
            GymMapper = new GymMapper(FloorMapper);
            EquipmentMapper = new EquipmentMapper();
            IdentityMapper = new IdentityMapper();
            EquipmentMaintenanceMapper = new EquipmentMaintenanceMapper();
        }
    }
}
