using FitnessGym.Application.Options;

namespace FitnessGym.API.Configs
{
    public static class ConfigureOptions
    {
        public static IServiceCollection ConfigureAppOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppOptions>(configuration.GetSection("Jwt"));
            services.Configure<AppOptions>(configuration.GetSection("DuendeClient"));

            return services;
        }
    }
}
