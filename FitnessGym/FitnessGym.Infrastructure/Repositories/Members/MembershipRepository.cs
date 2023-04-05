using FitnessGym.Domain.Entities.Members;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Repositories.Interfaces.Members;

namespace FitnessGym.Infrastructure.Repositories.Members
{
    public class MembershipRepository : GenericRepository<Membership>, IMembershipRepository
    {
        public MembershipRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
