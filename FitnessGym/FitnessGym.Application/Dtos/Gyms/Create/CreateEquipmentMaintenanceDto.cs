using System.Text.Json.Serialization;

namespace FitnessGym.Application.Dtos.Gyms.Create
{
    public class CreateEquipmentMaintenanceDto : EquipmentMaintenanceDto
    {
        [JsonIgnore]
        public override Guid Id { get; set; }
    }
}
