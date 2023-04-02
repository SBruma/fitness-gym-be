using FitnessGym.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessGym.Domain.Configurations.Members
{
    public class ApplicationUserConfiguration : AuditableEntityConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            base.Configure(builder);

            builder.HasIndex(user => user.Id)
                .IsUnique();

            builder.Property(user => user.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(user => user.LastName)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(user => user.DateOfBirth)
                .HasColumnType("date")
                .IsRequired();
            builder.Property(user => user.PhoneNumber)
                .HasMaxLength(15);
            builder.Property(user => user.EmergencyPhoneNumber)
                .HasMaxLength(15)
                .IsRequired(false);
            builder.Property(user => user.ProfilePicture)
                .IsRequired(false);
            builder.Property(user => user.Gender)
                .IsRequired();
        }
    }
}
