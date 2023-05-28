using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FluentResults;

namespace FitnessGym.Application.Services.Interfaces.Gyms
{
    public interface IMembershipService
    {
        Task<Result<MembershipDto>> Create(CreateMembershipDto createMembershipDto);
        Task<Result<MembershipDto>> GetActiveMembership(string userEmail);
        Task<Result<List<MembershipDto>>> GetHistory(string userEmail);
    }
}
