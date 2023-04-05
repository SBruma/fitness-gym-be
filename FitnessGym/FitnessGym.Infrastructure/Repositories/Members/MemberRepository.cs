using FitnessGym.Domain.Entities.Identity;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Repositories.Interfaces.Members;

namespace FitnessGym.Infrastructure.Repositories.Members
{
    public class MemberRepository : GenericRepository<ApplicationUser>, IMemberRepository
    {
        public MemberRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
