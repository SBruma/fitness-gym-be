using FitnessGym.Domain.Configurations;
using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
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
        }
    }
}
