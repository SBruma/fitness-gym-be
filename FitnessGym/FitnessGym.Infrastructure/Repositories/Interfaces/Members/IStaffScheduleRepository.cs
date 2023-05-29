using FitnessGym.Domain.Entities.Members;
using FluentResults;

namespace FitnessGym.Infrastructure.Repositories.Interfaces.Members
{
    public interface IStaffScheduleRepository : IGenericRepository<StaffSchedule>
    {
        Task<Result<List<StaffSchedule>>> GetStaffSchedule(Guid staffId);
    }
}
