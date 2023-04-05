using FitnessGym.Domain.Entities.Gyms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessGym.Domain.Configurations.Gyms
{
    public class MaintenanceHistoryConfiguration : AuditableEntityConfiguration<MaintenanceHistory>
    {
        const string MAINTENANCE_CONSTRAINT_NAME = "CK_Maintenance_Interval";
        const string MAINTENANCE_CONSTRAINT = "(\"StartDate\" < \"EndDate\")";

        public void Configure(EntityTypeBuilder<MaintenanceHistory> builder)
        {
            base.Configure(builder);

            builder.ToTable(t => t.HasCheckConstraint(MAINTENANCE_CONSTRAINT_NAME, MAINTENANCE_CONSTRAINT));

            builder.Property(m => m.Id)
                .HasConversion(mId => mId.Value,
                                value => new MaintenanceHistoryId(value));

            builder.HasKey(m => m.Id);
            builder.HasIndex(m => m.Id)
                .IsUnique();

            builder.Property(m => m.StartDate).IsRequired();
            builder.Property(m => m.EndDate).IsRequired();
            builder.Property(m => m.Description).IsRequired(false);
            builder.Property(m => m.Cost).IsRequired(false);

            builder.HasOne(m => m.Equipment)
                .WithMany(equipment => equipment.MaintenanceHistory)
                .HasForeignKey(m => m.EquipmentId);
        }
    }
}
