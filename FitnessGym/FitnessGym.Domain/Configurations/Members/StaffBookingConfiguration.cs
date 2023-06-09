﻿using FitnessGym.Domain.Entities.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessGym.Domain.Configurations.Members
{
    public class StaffBookingConfiguration : AuditableEntityConfiguration<StaffBooking>
    {
        public void Configure(EntityTypeBuilder<StaffBooking> builder)
        {
            base.Configure(builder);

            builder.ToTable("StaffBookings");

            builder.HasKey(sb => sb.Id);
            builder.HasIndex(sb => sb.Id)
               .IsUnique();

            builder.HasOne(sb => sb.Staff)
                .WithMany()
                .HasForeignKey(sb => sb.StaffId);
            builder.HasOne(sb => sb.Member)
                .WithMany()
                .HasForeignKey(sb => sb.MemberId);

            builder.Property(sb => sb.Id)
                .HasConversion(staffBookingId => staffBookingId.Value,
                                value => new StaffBookingId(value))
                .HasDefaultValueSql("uuid_generate_v4()")
                .IsRequired();
            builder.Property(sb => sb.SessionStart)
                .IsRequired();
            builder.Property(sb => sb.SessionEnd)
                .IsRequired();
        }
    }
}
