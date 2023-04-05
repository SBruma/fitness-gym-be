using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Repositories.Interfaces.Gyms;

namespace FitnessGym.Infrastructure.Repositories.Gyms
{
    public class MaintenanceHistoryRepository : GenericRepository<MaintenanceHistory>, IMaintenanceHistoryRepository
    {
        public MaintenanceHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
