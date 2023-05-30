using System.Text.Json.Serialization;

namespace FitnessGym.Application.Dtos.Gyms.Update
{
    public class UpdateStaffSchedule : StaffScheduleDto
    {
        [JsonIgnore]
        public override Guid MemberId { get; set; }
    }
}
