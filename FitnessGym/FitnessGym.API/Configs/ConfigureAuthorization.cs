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

                options.AddPolicy(Policies.Staff, policy => policy.RequireRole(Roles.Staff));

                options.AddPolicy(Policies.StaffTrainer, policy =>
                {
                    policy.RequireRole(Roles.Staff);
                    policy.RequireRole(Roles.Trainer);
                });

                options.AddPolicy(Policies.StaffTehnician, policy =>
                {
                    policy.RequireRole(Roles.Staff);
                    policy.RequireRole(Roles.Tehnician);
                });

                options.AddPolicy(Policies.StaffReceptionist, policy =>
                {
                    policy.RequireRole(Roles.Staff);
                    policy.RequireRole(Roles.Receptionist);
                });

                options.AddPolicy(Policies.StaffManager, policy =>
                {
                    policy.RequireRole(Roles.Staff);
                    policy.RequireRole(Roles.Manager);
                });
            });

            return services;
        }
    }

    public static class Policies
    {
        public const string Member = "Member";
        public const string Staff = "Staff";
        public const string StaffTrainer = "StaffTrainer";
        public const string StaffTehnician = "StaffTehnician";
        public const string StaffReceptionist = "StaffReceptionist";
        public const string StaffManager = "StaffManager";
    }
}
