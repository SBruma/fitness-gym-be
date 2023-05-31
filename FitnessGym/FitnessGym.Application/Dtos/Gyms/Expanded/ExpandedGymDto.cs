using FitnessGym.Application.Dtos.Gyms.Entities;

namespace FitnessGym.Application.Dtos.Gyms
{
    public class ExpandedGymDto : GymEntityDto
    {
        public List<FloorDto> Floors { get; set; }
    }
}
