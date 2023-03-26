using FitnessGym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessGym.Domain.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(user => user.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(user => user.LastName)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(user => user.DateOfBirth)
                .HasColumnType("date")
                .IsRequired();
            builder.Property(user => user.EmergencyPhoneNumber)
                .HasMaxLength(15)
                .IsRequired(false);
            builder.Property(user => user.ProfilePicture)
                .IsRequired(false);
            builder.Property(user => user.Gender)
                .IsRequired();
            builder.Property(user => user.CreationDate)
                .IsRequired();
        }
    }
}
