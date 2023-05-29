using FitnessGym.Application.Dtos.Gyms;
using FluentResults;

namespace FitnessGym.Application.Services.Interfaces.Gyms
{
    public interface IStaffScheduleService
    {
        Task<Result<StaffScheduleDto>> CreateSchedule(StaffScheduleDto staffScheduleDto);
        Task<Result<StaffScheduleDto>> GetStaffSchedule(Guid staffId);
    }
}
