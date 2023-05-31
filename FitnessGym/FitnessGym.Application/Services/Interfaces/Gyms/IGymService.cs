using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Update;
using FitnessGym.Domain.Entities.Gyms;
using FluentResults;

namespace FitnessGym.Application.Services.Interfaces.Gyms
{
    public interface IGymService
    {
        Task<Result<GymDto>> Create(CreateGymDto createGymDto);
        Task<Result<GymDto>> Update(GymId gymToUpdateId, UpdateDetailsGymDto updateGymDto);
        Task<Result> Delete(GymId gymId);
        Task<Result<ExpandedGymDto>> Get(GymId gymId);
        Task<Result<List<GymDto>>> GetAll();
    }
}
