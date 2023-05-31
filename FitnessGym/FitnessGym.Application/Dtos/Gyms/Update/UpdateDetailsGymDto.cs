using FitnessGym.Application.Dtos.Gyms.Entities;
using System.Text.Json.Serialization;

namespace FitnessGym.Application.Dtos.Gyms.Update
{
    public class UpdateDetailsGymDto : GymEntityDto
    {
        [JsonIgnore]
        public override Guid Id { get; set; }
    }
}
