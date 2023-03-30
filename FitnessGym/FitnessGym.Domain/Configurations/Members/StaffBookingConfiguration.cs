using FitnessGym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessGym.Domain.Configurations.Members
{
    public class StaffBookingConfiguration : IEntityTypeConfiguration<StaffBooking>
    {
        public void Configure(EntityTypeBuilder<StaffBooking> builder)
        {
            builder.ToTable("StaffBookings");
            builder.HasKey(sb => sb.Id);
            builder.HasOne(sb => sb.Staff)
                .WithMany()
                .HasForeignKey(sb => sb.StaffId);
            builder.HasOne(sb => sb.Member)
                .WithMany()
                .HasForeignKey(sb => sb.MemberId);

            builder.Property(sb => sb.Id)
                .HasConversion(staffBookingId => staffBookingId.Value,
                value => new StaffBookingId(value))
                .IsRequired();
            builder.Property(sb => sb.SessionStart)
                .IsRequired();
            builder.Property(sb => sb.SessionEnd)
                .IsRequired();
        }
    }
}
