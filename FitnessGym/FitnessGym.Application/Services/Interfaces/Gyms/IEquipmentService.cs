using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Application.Dtos.Gyms.Expanded;
using FitnessGym.Application.Dtos.Gyms.Update;
using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Domain.Filters;
using FluentResults;

namespace FitnessGym.Application.Services.Interfaces.Gyms
{
    public interface IEquipmentService
    {
        Task<Result<EquipmentDto>> Create(CreateEquipmentDto createEquipmentDto);
        Task<Result<List<EquipmentDto>>> Create(List<CreateEquipmentDto> createEquipmentDtos);
        Task<Result<ExpandedEquipmentDto>> Update(EquipmentId equipmentId, UpdateEquipmentDto updateEquipmentDto);
        Task<Result> Delete(EquipmentId equipmentId);
        Task<Result<ExpandedEquipmentDto>> GetById(EquipmentId equipmentId);
        Task<Result<List<EquipmentDto>>> GetFiltered(EquipmentFilter equipmentFilter, PaginationFilter paginationFilter);
    }
}
