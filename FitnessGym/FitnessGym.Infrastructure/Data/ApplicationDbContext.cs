using FitnessGym.Domain.Configurations;
using FitnessGym.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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
        }
    }
}
