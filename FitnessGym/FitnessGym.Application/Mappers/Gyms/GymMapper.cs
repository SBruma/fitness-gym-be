using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Domain.Entities.Gyms;
using Riok.Mapperly.Abstractions;

namespace FitnessGym.Application.Mappers.Gyms
{
    [Mapper]
    public partial class GymMapper
    {
        public partial Gym CreateGymToGym(CreateGymDto createGymDto);
        public partial void UpdateGymToGym(UpdateGymDto updateGymDto, Gym gym);
        public partial GymDto GymToGymDto(Gym gym);

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
    }
}
