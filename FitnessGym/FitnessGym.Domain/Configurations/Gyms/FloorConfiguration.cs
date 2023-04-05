using FitnessGym.Domain.Entities.Gyms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessGym.Domain.Configurations.Gyms
{
    public class FloorConfiguration : AuditableEntityConfiguration<Floor>
    {
        public void Configure(EntityTypeBuilder<Floor> builder)
        {
            base.Configure(builder);

            builder.ToTable("Floors");

            builder.HasOne(floor => floor.Gym)
                .WithMany(gym => gym.Floors)
                .HasForeignKey(floor => floor.GymId);

            builder.HasKey(floor => new { floor.GymId, floor.Level });
            builder.HasIndex(floor => new { floor.GymId, floor.Level })
                .IsUnique();
        }
    }
}
