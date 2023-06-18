using FitnessGym.Domain.Entities.Gyms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessGym.Domain.Configurations.Gyms
{
    public class GymCheckInConfiguration : AuditableEntityConfiguration<GymCheckIn>
    {
        public void Configure(EntityTypeBuilder<GymCheckIn> builder)
        {
            base.Configure(builder);

            builder.ToTable("GymCheckIns");

            builder.HasOne(gymCheckIn => gymCheckIn.Membership)
                .WithMany(membership => membership.GymCheckIns)
                .HasForeignKey(gymCheckIn => gymCheckIn.MembershipId);

            builder.Property(gymCheckIn => gymCheckIn.CheckInTime)
                .IsRequired();

            builder.Property(gymCheckIn => gymCheckIn.CheckOutTime)
                .IsRequired(false);
        }
    }
}
