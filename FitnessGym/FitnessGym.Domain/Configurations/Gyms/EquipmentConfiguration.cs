using FitnessGym.Domain.Entities.Gyms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessGym.Domain.Configurations.Gyms
{
    public class EquipmentConfiguration : AuditableEntityConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
            base.Configure(builder);

            builder.ToTable("Equipments");

            builder.Property(equipment => equipment.Id)
                .HasConversion(equipmentId => equipmentId.Value,
                                value => new EquipmentId(value))
                .HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(equipment => equipment.GymId)
                .HasConversion(gymId => gymId.Value,
                                value => new GymId(value));

            builder.HasKey(equipment => equipment.Id);
            builder.HasIndex(equipment => equipment.Id)
                .IsUnique();

            builder.HasOne(equipment => equipment.Floor)
                .WithMany(floor => floor.Equipments)
                .HasForeignKey(equipment => new { equipment.GymId, equipment.Level })
                .IsRequired(false);

            builder.Property(equipment => equipment.Name)
                .HasMaxLength(75)
                .IsRequired();

            builder.Property(equipment => equipment.SerialNumber)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(equipment => equipment.ModelNumber)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(equipment => equipment.Description)
                .HasMaxLength(250)
                .IsRequired(false);

            builder.Property(equipment => equipment.Category).IsRequired();
            builder.Property(equipment => equipment.Status).IsRequired();
            builder.Property(equipment => equipment.PurchaseDate).IsRequired();
            builder.Property(equipment => equipment.WarrantyExpirationDate).IsRequired();

            builder.OwnsOne(equipment => equipment.FloorLocation, floorLocation =>
            {
                floorLocation.Property(floorLocation => floorLocation.Row).IsRequired();
                floorLocation.Property(floorLocation => floorLocation.Column).IsRequired();
            });
        }
    }
}
