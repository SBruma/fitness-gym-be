using FitnessGym.Application.Dtos.Gyms.Entities;
using FitnessGym.Domain.Entities.Gyms;
using System.Text.Json.Serialization;

namespace FitnessGym.Application.Dtos.Gyms.Create
{
    public class CreateFloorDto : FloorEntityDto
    {
        [JsonIgnore]
        public override GymId GymId { get; set; }
    }
}
