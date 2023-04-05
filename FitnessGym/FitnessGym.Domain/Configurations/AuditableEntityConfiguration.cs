using FitnessGym.Domain.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessGym.Domain.Configurations
{
    public class AuditableEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IAuditableEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
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
