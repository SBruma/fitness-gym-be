using FitnessGym.Application.Mappers.Gyms;

namespace FitnessGym.Application.Mappers
{
    public class Mapper : IMapper
    {
        public FloorMapper FloorMapper { get; }
        public GymMapper GymMapper { get; }

        public Mapper()
        {
            FloorMapper = new FloorMapper();
            GymMapper = new GymMapper(FloorMapper);
        }
    }
}
