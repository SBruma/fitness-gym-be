using FitnessGym.Application.Mappers;
using FitnessGym.Application.Services.Gyms;
using FitnessGym.Application.Services.Identity;
using FitnessGym.Application.Services.Interfaces.Gyms;
using FitnessGym.Application.Services.Interfaces.Identity;
using FitnessGym.Application.Services.Interfaces.Others;
using FitnessGym.Application.Services.Others;
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
            services.AddTransient<IEquipmentMaintenanceService, EquipmentMaintenanceService>();
            services.AddTransient<IMembershipService, MembershipService>();
            services.AddTransient<IStaffScheduleService, StaffScheduleService>();
            services.AddTransient<IStaffBookingService, StaffBookingService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IMapper, Mapper>();

            return services;
        }
    }
}
