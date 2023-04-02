using FitnessGym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessGym.Domain.Configurations
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

            builder.ToTable("StaffSchedules");
            builder.HasKey(schedule => new { schedule.StaffId, schedule.DayOfWeek });
            builder.HasIndex(schedule => new { schedule.StaffId, schedule.DayOfWeek }).IsUnique();

            builder.HasOne(schedule => schedule.Staff)
                .WithMany(user => user.StaffSchedule)
                .HasForeignKey(schedule => schedule.StaffId);

            builder.ToTable(t => t.HasCheckConstraint(SCHEDULE_CONSTRAINT_NAME, SCHEDULE_CONSTRAINT));
            builder.Property(schedule => schedule.StartTime).HasDefaultValue(TimeSpan.FromHours(START_TIME)).IsRequired();
            builder.Property(schedule => schedule.BreakStartTime).HasDefaultValue(TimeSpan.FromHours(BREAK_START_TIME)).IsRequired();
            builder.Property(schedule => schedule.BreakEndTime).HasDefaultValue(TimeSpan.FromHours(BREAK_END_TIME)).IsRequired();
            builder.Property(schedule => schedule.EndTime).HasDefaultValue(TimeSpan.FromHours(END_TIME)).IsRequired();
        }
    }
}
