using FitnessGym.Domain.Entities.Identity;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Data.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessGym.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DependencyInjection).Assembly.GetName().Name;
            var connectionString = configuration.GetConnectionString("DatabaseConnection") ?? string.Empty;
            services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var auditableInterceptor = services.BuildServiceProvider().GetService<UpdateAuditableEntitiesInterceptor>()!;

                options.UseNpgsql(connectionString, sql => sql.MigrationsAssembly(assembly))
                    .AddInterceptors(auditableInterceptor);
            });

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    options.EmitStaticAudienceClaim = true;
                })
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseNpgsql(connectionString,
                        sql => sql.MigrationsAssembly(assembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseNpgsql(connectionString,
                        sql => sql.MigrationsAssembly(assembly));
                    options.EnableTokenCleanup = true;
                    options.RemoveConsumedTokens = true;
                })
                .AddAspNetIdentity<ApplicationUser>();

            return services;
        }
    }
}
