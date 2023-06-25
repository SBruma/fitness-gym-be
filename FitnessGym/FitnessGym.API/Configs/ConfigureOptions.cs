using FitnessGym.Application.Options;

namespace FitnessGym.API.Configs
{
    public static class ConfigureOptions
    {
        public static IServiceCollection ConfigureAppOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppOptions>(configuration.GetSection("Jwt"));
            services.Configure<AppOptions>(configuration.GetSection("Google"));
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.Configure<AppOptions>(configuration.GetSection("FrontEnd"));

            return services;
        }
    }
}
