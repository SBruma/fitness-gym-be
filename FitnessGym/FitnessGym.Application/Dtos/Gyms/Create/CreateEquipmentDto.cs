using FitnessGym.Application.Dtos.Gyms.Entities;
using System.Text.Json.Serialization;

namespace FitnessGym.Application.Dtos.Gyms.Create
{
    public class CreateEquipmentDto : EquipmentEntityDto
    {
        [JsonIgnore]
        public override Guid Id { get; set; }
    }
}
