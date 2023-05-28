using FitnessGym.Application.Mappers.Gyms;
using FitnessGym.Application.Mappers.Identity;

namespace FitnessGym.Application.Mappers
{
    public interface IMapper
    {
        GymMapper GymMapper { get; }
        FloorMapper FloorMapper { get; }
        EquipmentMapper EquipmentMapper { get; }
        IdentityMapper IdentityMapper { get; }
        EquipmentMaintenanceMapper EquipmentMaintenanceMapper { get; }
        MembershipMapper MembershipMapper { get; }
    }
}
