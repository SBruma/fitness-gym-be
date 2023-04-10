using FitnessGym.Application.Mappers.Gyms;

namespace FitnessGym.Application.Mappers
{
    public class Mapper : IMapper
    {
        public GymMapper GymMapper => new();

        public FloorMapper FloorMapper => new();
    }
}
