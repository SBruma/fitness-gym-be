using FitnessGym.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.API.Configs
{
    public static class ConfigureMigrations
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
