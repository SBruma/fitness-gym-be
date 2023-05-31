using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Domain.Entities.Members;
using FitnessGym.Domain.Filters;
using FluentResults;

namespace FitnessGym.Application.Services.Interfaces.Gyms
{
    public interface IStaffBookingService
    {
        Task<Result<StaffBookingDto>> Create(CreateStaffBookingDto dto);
        Task<Result<StaffBookingDto>> GetById(StaffBookingId staffBookingId);
        Task<Result<List<StaffBookingDto>>> GetFitlered(StaffBookingFilter staffBookingFilter);
    }
}
