using FitnessGym.Application.Dtos.Gyms.Entities;
using FitnessGym.Domain.Entities.Gyms;
using System.Text.Json.Serialization;

namespace FitnessGym.Application.Dtos.Gyms.Create
{
    public class CreateEquipmentDto : EquipmentEntityDto
    {
        [JsonIgnore]
        public override EquipmentId Id { get; set; }
    }
}
