using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Domain.Entities.Members;
using FluentResults;

namespace FitnessGym.Infrastructure.Repositories.Interfaces.Gyms
{
    public interface IGymCheckInRepository : IGenericRepository<GymCheckIn>
    {
        Task<Result<GymCheckIn>> GetActive(MembershipId membershipId);
        Task<Result<List<GymCheckIn>>> GetHistory(DateTime minimumDate, GymId gymId);
        Task<Result<int>> GetMembersInGym(GymId gymId);
        Task<bool> CheckOutJob();
    }
}