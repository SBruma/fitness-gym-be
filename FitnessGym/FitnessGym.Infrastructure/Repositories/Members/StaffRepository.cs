using FitnessGym.Domain.Entities.Identity;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Repositories.Interfaces.Members;

namespace FitnessGym.Infrastructure.Repositories.Members
{
    public class StaffRepository : GenericRepository<ApplicationUser>, IStaffRepository
    {
        public StaffRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
