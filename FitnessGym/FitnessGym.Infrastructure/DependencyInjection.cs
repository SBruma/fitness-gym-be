using FitnessGym.Domain.Entities.Identity;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Data.Interceptors;
using FitnessGym.Infrastructure.Data.Interfaces;
using FitnessGym.Infrastructure.Repositories.Gyms;
using FitnessGym.Infrastructure.Repositories.Interfaces.Gyms;
using FitnessGym.Infrastructure.Repositories.Interfaces.Members;
using FitnessGym.Infrastructure.Repositories.Members;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
                options.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
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

            services.AddScoped<IGymRepository, GymRepository>();
            services.AddScoped<IFloorRepository, FloorRepository>();
            services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            services.AddScoped<IMaintenanceHistoryRepository, MaintenanceHistoryRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IMembershipRepository, MembershipRepository>();
            services.AddScoped<IStaffBookingRepository, StaffBookingRepository>();
            services.AddScoped<IStaffScheduleRepository, StaffScheduleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
