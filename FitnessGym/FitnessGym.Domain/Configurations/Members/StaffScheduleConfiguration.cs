using FitnessGym.Domain.Entities.Members;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessGym.Domain.Configurations.Members
{
    public class StaffScheduleConfiguration : AuditableEntityConfiguration<StaffSchedule>
    {
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
