using FitnessGym.Application.Mappers;
using FitnessGym.Application.Services.Gyms;
using FitnessGym.Application.Services.Identity;
using FitnessGym.Application.Services.Interfaces.Gyms;
using FitnessGym.Application.Services.Interfaces.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessGym.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IGymService, GymService>();
            services.AddTransient<IEquipmentService, EquipmentService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IMapper, Mapper>();

            return services;
        }
    }
}
