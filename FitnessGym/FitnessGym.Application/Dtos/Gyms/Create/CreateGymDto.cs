using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Application.Dtos.Gyms.Entities;
using System.Text.Json.Serialization;

namespace FitnessGym.Application.Dtos.Gyms
{
    public class CreateGymDto : GymEntityDto
    {
        [JsonIgnore]
        public override Guid Id { get; set; }
        public List<CreateFloorDto> Floors { get; set; }
    }
}
