using FitnessGym.Application.Services.Others;
using Hangfire;
using Hangfire.PostgreSql;

namespace FitnessGym.API.Configs
{
    public static class ConfigureHangFire
    {
        public static IServiceCollection ConfigureHangFireOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(options => options
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseColouredConsoleLogProvider()
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(configuration.GetConnectionString("DatabaseConnection"), new PostgreSqlStorageOptions
                {
                    QueuePollInterval = TimeSpan.FromSeconds(15)
                }));
            services.AddHangfireServer();
            services.AddScoped<HangFireService>();

            return services;
        }
    }

}
