using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Application.Dtos.Gyms.Expanded;
using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Domain.Filters;
using FluentResults;

namespace FitnessGym.Application.Services.Interfaces.Gyms
{
    public interface IEquipmentService
    {
        Task<Result<EquipmentDto>> Create(CreateEquipmentDto createEquipmentDto);
        //Task<Result<GymDto>> Update(EquipmentId equipmentId, UpdateDetailsGymDto updateGymDto);
        //Task<Result> Delete(EquipmentId equipmentId);
        //Task<Result<ExpandedEquipmentDto>> GetById(EquipmentId equipmentId);
        //Task<Result<List<EquipmentDto>>> Get(EquipmentFilter equipmentFilter);
    }
}
