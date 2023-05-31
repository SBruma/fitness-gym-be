namespace FitnessGym.Application.Dtos.Gyms.Entities
{
    public abstract class StaffScheduleEntityDto
    {
        public virtual Guid MemberId { get; set; }
        public List<ScheduleDto> Schedules { get; set; }
    }
}
