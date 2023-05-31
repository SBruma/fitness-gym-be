using FitnessGym.Domain.Entities.Members;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessGym.Domain.Configurations.Members
{
    public class StaffScheduleConfiguration : AuditableEntityConfiguration<StaffSchedule>
    {
        const int START_TIME = 8;
        const int END_TIME = 12;
        const int BREAK_START_TIME = 13;
        const int BREAK_END_TIME = 17;
        const string SCHEDULE_CONSTRAINT_NAME = "CK_Schedule_Interval";
        const string SCHEDULE_CONSTRAINT = "(\"StartTime\" < \"BreakStartTime\") AND " +
                                            "(\"BreakStartTime\" < \"BreakEndTime\") AND " +
                                            "(\"BreakEndTime\" < \"EndTime\")";

        public void Configure(EntityTypeBuilder<StaffSchedule> builder)
        {
            base.Configure(builder);

            builder.HasKey(schedule => new { schedule.StaffId, schedule.DayOfWeek });
            builder.HasIndex(schedule => new { schedule.StaffId, schedule.DayOfWeek }).IsUnique();

            builder.HasOne(schedule => schedule.Staff)
                .WithMany(user => user.StaffSchedule)
                .HasForeignKey(schedule => schedule.StaffId);

            builder.Property(schedule => schedule.StartTime).IsRequired(false);
            builder.Property(schedule => schedule.BreakStartTime).IsRequired(false);
            builder.Property(schedule => schedule.BreakEndTime).IsRequired(false);
            builder.Property(schedule => schedule.EndTime).IsRequired(false);
        }
    }
}
