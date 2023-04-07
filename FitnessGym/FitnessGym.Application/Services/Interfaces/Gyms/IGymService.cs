using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Domain.Entities.Gyms;
using FluentResults;

namespace FitnessGym.Application.Services.Interfaces.Gyms
{
    public interface IGymService
    {
        Task<Result<GymDto>> Create(CreateGymDto createGymDto);
        Task<Result<GymDto>> Update(GymId gymToUpdateId, UpdateGymDto updateGymDto);
        Task<Result> Delete(GymId gymId);
        Task<Result<GymDto>> Get(GymId gymId);
        Task<Result<List<GymDto>>> GetAll();
    }
}
