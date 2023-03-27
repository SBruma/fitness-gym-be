using FitnessGym.Domain.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessGym.Domain.Configurations
{
    internal class AuditableEntityConfiguration : IEntityTypeConfiguration<IAuditableEntity>
    {
        public void Configure(EntityTypeBuilder<IAuditableEntity> builder)
        {
            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();
            builder.Property(e => e.CreatedOnUtc)
                .IsRequired();
            builder.Property(e => e.ModifiedOnUtc)
                .IsRequired(false);
        }
    }
}
