using FitnessGym.Domain.Entities.Gyms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessGym.Domain.Configurations.Gyms
{
    public class MaintenanceHistoryConfiguration : AuditableEntityConfiguration<MaintenanceHistory>
    {
        public void Configure(EntityTypeBuilder<MaintenanceHistory> builder)
        {
            base.Configure(builder);

            builder.Property(m => m.Id)
                .HasConversion(mId => mId.Value,
                                value => new MaintenanceHistoryId(value))
                .HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(m => m.EquipmentId)
                .HasConversion(m => m.Value,
                                value => new EquipmentId(value));

            builder.HasKey(m => m.Id);
            builder.HasIndex(m => m.Id)
                .IsUnique();

            builder.Property(m => m.StartDate).IsRequired();
            builder.Property(m => m.EndDate).IsRequired(false);
            builder.Property(m => m.Description).IsRequired(false);
            builder.Property(m => m.Cost).IsRequired(false);

            builder.HasOne(m => m.Equipment)
                .WithMany(equipment => equipment.MaintenanceHistory)
                .HasForeignKey(m => m.EquipmentId);
        }
    }
}
