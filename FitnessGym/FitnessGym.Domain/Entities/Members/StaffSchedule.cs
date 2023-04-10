using FitnessGym.Domain.Entities.Identity;
using FitnessGym.Domain.Entities.Interfaces;

namespace FitnessGym.Domain.Entities.Members
{
    public class StaffSchedule : IAuditableEntity
    {
        public Guid StaffId { get; set; }
        public ApplicationUser Staff { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan BreakStartTime { get; set; }
        public TimeSpan BreakEndTime { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
    }
}
