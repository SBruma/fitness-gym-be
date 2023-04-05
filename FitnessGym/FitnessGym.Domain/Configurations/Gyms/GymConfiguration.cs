using FitnessGym.Domain.Entities.Gyms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessGym.Domain.Configurations.Gyms
{
    public class GymConfiguration : AuditableEntityConfiguration<Gym>
    {
        public void Configure(EntityTypeBuilder<Gym> builder)
        {
            base.Configure(builder);

            builder.ToTable("Gyms");

            builder.HasKey(gym => gym.Id);
            builder.HasIndex(gym => gym.Id)
                .IsUnique();

            builder.Property(gym => gym.Id)
                .HasConversion(gymId => gymId.Value,
                                value => new GymId(value));

            builder.Property(gym => gym.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(gym => gym.EmailAddress)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(gym => gym.PhoneNumber)
                .HasMaxLength(15)
                .IsRequired();

            builder.OwnsOne(gym => gym.GeoCoordinate, gym =>
            {
                gym.Property(gc => gc.Latitude).IsRequired();
                gym.Property(gc => gc.Longitude).IsRequired();
            });

            builder.OwnsOne(gym => gym.Address, gym =>
            {
                gym.Property(adress => adress.Country).IsRequired();
                gym.Property(adress => adress.City).IsRequired();
                gym.Property(adress => adress.Street).IsRequired();
                gym.Property(adress => adress.BuildingNumber).IsRequired(false);
            });

            builder.OwnsOne(gym => gym.Layout, gym =>
            {
                gym.Property(layout => layout.Length).IsRequired();
                gym.Property(layout => layout.Width).IsRequired();
                gym.Property(layout => layout.FloorNumber).IsRequired();
            });
        }
    }
}
