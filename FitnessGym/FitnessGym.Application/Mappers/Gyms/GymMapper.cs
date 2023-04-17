using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Domain.Entities.Gyms;
using Riok.Mapperly.Abstractions;

namespace FitnessGym.Application.Mappers.Gyms
{
    [Mapper]
    public partial class GymMapper
    {
        private readonly FloorMapper FloorMapper;
        public GymMapper(FloorMapper floorMapper)
        {
            FloorMapper = floorMapper;
        }

        [MapperIgnoreSource(nameof(CreateGymDto.Floors))]
        public partial Gym CreateGymToGym(CreateGymDto createGymDto);
        [MapperIgnoreSource(nameof(UpdateDetailsGymDto.Id))]
        public partial void UpdateGymToGym(UpdateDetailsGymDto updateGymDto, Gym gym);
        public partial GymDto GymToGymDto(Gym gym);
        public partial ExpandedGymDto GymToExpandedGymDto(Gym gym);

        public List<GymDto> GymsToGymsDto(List<Gym> gyms)
        {
            return gyms.Select(MapGymToGymDto).ToList();
        }

        public GymDto MapGymToGymDto(Gym gym)
        {
            var dto = GymToGymDto(gym);
            dto.Id = gym.Id.Value;

            return dto;
        }

        public ExpandedGymDto MapGymToExpandedGymDto(Gym gym)
        {
            var dto = GymToExpandedGymDto(gym);
            dto.Id = gym.Id.Value;
            dto.Floors = FloorMapper.FloorDtoFloor(gym.Floors);

            return dto;
        }
    }
}
