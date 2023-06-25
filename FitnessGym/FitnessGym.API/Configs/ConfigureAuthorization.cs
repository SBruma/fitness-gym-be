using FitnessGym.Domain.Entities.Statics;

namespace FitnessGym.API.Configs
{
    public static class ConfigureAuthorization
    {
        public static IServiceCollection ConfigureAuthorizationOptions(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.Member, policy => policy.RequireRole(Roles.Member));
                options.AddPolicy(Policies.Trainer, policy => policy.RequireRole(Roles.Trainer));
                options.AddPolicy(Policies.Technician, policy => policy.RequireRole(Roles.Technician));
                options.AddPolicy(Policies.Receptionist, policy => policy.RequireRole(Roles.Receptionist));
                options.AddPolicy(Policies.Manager, policy => policy.RequireRole(Roles.Manager));
            });

            return services;
        }
    }

    public static class Policies
    {
        public const string Member = "Member";
        public const string Trainer = "Trainer";
        public const string Technician = "Technician";
        public const string Receptionist = "Receptionist";
        public const string Manager = "Manager";
    }
}
