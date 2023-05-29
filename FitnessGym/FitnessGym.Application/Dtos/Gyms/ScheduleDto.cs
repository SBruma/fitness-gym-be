namespace FitnessGym.Application.Dtos.Gyms
{
    public class ScheduleDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public TimeOnly? BreakStartTime { get; set; }
        public TimeOnly? BreakEndTime { get; set; }
    }
}
