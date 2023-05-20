using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Extensions;
using FitnessGym.Domain.Configurations.Gyms;
using FitnessGym.Domain.Configurations.Members;
using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Domain.Entities.Identity;
using FitnessGym.Domain.Entities.Members;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<PersistedGrant> PersistedGrants { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresExtension("uuid-ossp");
            ConfigureEntities(builder);

            base.OnModelCreating(builder);
        }

        private void ConfigureEntities(ModelBuilder builder)
        {
            new ApplicationUserConfiguration().Configure(builder.Entity<ApplicationUser>());
            new StaffBookingConfiguration().Configure(builder.Entity<StaffBooking>());
            new StaffScheduleConfiguration().Configure(builder.Entity<StaffSchedule>());
            new GymConfiguration().Configure(builder.Entity<Gym>());
            new MembershipConfiguration().Configure(builder.Entity<Membership>());
            new FloorConfiguration().Configure(builder.Entity<Floor>());
            new EquipmentConfiguration().Configure(builder.Entity<Equipment>());
            new MaintenanceHistoryConfiguration().Configure(builder.Entity<MaintenanceHistory>());
        }
    }
}
