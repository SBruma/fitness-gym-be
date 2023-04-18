using FitnessGym.Application.Mappers.Gyms;

namespace FitnessGym.Application.Mappers
{
    public interface IMapper
    {
        GymMapper GymMapper { get; }
        FloorMapper FloorMapper { get; }
        EquipmentMapper EquipmentMapper { get; }
    }
}
