using FitnessGym.Application.Dtos.Gyms.Entities;
using System.Text.Json.Serialization;

namespace FitnessGym.Application.Dtos.Gyms
{
    public class UpdateDetailsGymDto : GymEntityDto
    {
        [JsonIgnore]
        public override Guid Id { get; set; }
    }
}
