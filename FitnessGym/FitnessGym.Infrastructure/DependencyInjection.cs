using FitnessGym.Domain.Entities.Identity;
using FitnessGym.Infrastructure.Configs;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Data.CustomTokenProviders;
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

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<EmailConfirmationTokenProvider<ApplicationUser>>("emailconfirmation");

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2));
            services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromDays(3));

            //services.AddIdentityServer(options =>
            //    {
            //        options.Events.RaiseErrorEvents = true;
            //        options.Events.RaiseInformationEvents = true;
            //        options.Events.RaiseFailureEvents = true;
            //        options.Events.RaiseSuccessEvents = true;
            //        options.EmitStaticAudienceClaim = true;
            //    })
            //    .AddConfigurationStore(options =>
            //    {
            //        options.ConfigureDbContext = b => b.UseNpgsql(connectionString,
            //            sql => sql.MigrationsAssembly(assembly));
            //    })
            //    .AddOperationalStore(options =>
            //    {
            //        options.ConfigureDbContext = b => b.UseNpgsql(connectionString,
            //            sql => sql.MigrationsAssembly(assembly));
            //        options.EnableTokenCleanup = true;
            //        options.RemoveConsumedTokens = true;
            //    })
            //    .AddAspNetIdentity<ApplicationUser>()
            //    .AddInMemoryClients(IdentityConfig.Clients(configuration))  // Configure clients
            //    .AddInMemoryIdentityResources(IdentityConfig.IdentityResources)  // Configure identity resources
            //    .AddInMemoryApiScopes(IdentityConfig.ApiScopes)  // Configure API scopes
            //    .AddInMemoryApiResources(IdentityConfig.ApiResources);  // Configure API resources;

            services.AddScoped<IGymRepository, GymRepository>();
            services.AddScoped<IFloorRepository, FloorRepository>();
            services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            services.AddScoped<IMaintenanceHistoryRepository, MaintenanceHistoryRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IMembershipRepository, MembershipRepository>();
            services.AddScoped<IStaffBookingRepository, StaffBookingRepository>();
            services.AddScoped<IStaffScheduleRepository, StaffScheduleRepository>();
            services.AddScoped<IGymCheckInRepository, GymCheckInsRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
