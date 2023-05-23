using System.Text.Json.Serialization;

namespace FitnessGym.Application.Dtos.Gyms.Update
{
    public class UpdateEquipmentDto : EquipmentDto
    {
        [JsonIgnore]
        public override Guid Id { get; set; }
    }
}
