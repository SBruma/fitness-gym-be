using System.Text.Json.Serialization;

namespace FitnessGym.Application.Dtos.Gyms.Update
{
    public class UpdateEquipmentMaintenanceDto : EquipmentMaintenanceDto
    {
        [JsonIgnore]
        public override Guid Id { get; set; }
        [JsonIgnore]
        public override Guid EquipmentId { get; set; }
    }
}
