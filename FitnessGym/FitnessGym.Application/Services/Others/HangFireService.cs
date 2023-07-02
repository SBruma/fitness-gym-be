using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Infrastructure.Data.Interfaces;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessGym.Application.Services.Others
{
    public class HangFireService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public HangFireService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            RecurringJob.AddOrUpdate("membershipCheckOutJob", () => StartCheckOutValidationJobAsync(), Cron.Hourly);
            RecurringJob.AddOrUpdate("equipmentWarrantyCheckJob", () => StartWarrantyValdiationJobsAsync(), Cron.Hourly);
        }

        public async Task StartCheckOutValidationJobAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                if (await unitOfWork.GymCheckInRepository.CheckOutJob())
                {
                    await unitOfWork.SaveChangesAsync();
                }
            }
        }

        public async Task StartWarrantyValdiationJobsAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                if (await unitOfWork.EquipmentRepository.ValidateWarrantyExpirationJob())
                {
                    await unitOfWork.SaveChangesAsync();
                }
            }
        }
    }
}
