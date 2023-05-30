using System.Text.Json.Serialization;

namespace FitnessGym.Application.Dtos.Gyms.Create
{
    public class CreateStaffBookingDto : StaffBookingDto
    {
        [JsonIgnore]
        public override Guid Id { get; set; }
        [JsonIgnore]
        public override DateTime SessionEnd { get; set; }
        public int Duration { get; set; }
    }
}
