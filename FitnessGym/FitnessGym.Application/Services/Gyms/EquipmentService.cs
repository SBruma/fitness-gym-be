using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Application.Dtos.Gyms.Expanded;
using FitnessGym.Application.Dtos.Gyms.Update;
using FitnessGym.Application.Errors;
using FitnessGym.Application.Errors.Gyms;
using FitnessGym.Application.Mappers;
using FitnessGym.Application.Services.Interfaces.Gyms;
using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Domain.Filters;
using FitnessGym.Infrastructure.Data.Interfaces;
using FluentResults;

namespace FitnessGym.Application.Services.Gyms
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EquipmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<EquipmentDto>> Create(CreateEquipmentDto createEquipmentDto)
        {
            var equipment = _mapper.EquipmentMapper.MapCreateEquipmentToEquipment(createEquipmentDto);
            var result = await _unitOfWork.EquipmentRepository.Add(equipment);

            if (result.IsFailed)
            {
                return Result.Fail(new EquipmentNotCreatedError());
            }

            await _unitOfWork.SaveChangesAsync();

            return Result.Ok(_mapper.EquipmentMapper.MapEquipmentToEquipmentDto(equipment));
        }

        public async Task<Result> Delete(EquipmentId equipmentId)
        {
            var retrieveResult = await _unitOfWork.EquipmentRepository.GetById(equipmentId);

            if (retrieveResult.IsFailed)
            {
                return Result.Fail(new EquipmentNotFound(equipmentId));
            }

            var deleteResult = _unitOfWork.EquipmentRepository.Delete(retrieveResult.Value);
            await _unitOfWork.SaveChangesAsync();

            return deleteResult.IsSuccess ? Result.Ok() : Result.Fail(new DeleteError(equipmentId.Value));
        }

        public async Task<Result<List<EquipmentDto>>> Get(EquipmentFilter equipmentFilter, PaginationFilter paginationFilter)
        {
            var equipments = await _unitOfWork.EquipmentRepository.GetFiltered(equipmentFilter, paginationFilter);

            return Result.Ok(_mapper.EquipmentMapper.EquipmentsToEquipmentsDto(equipments));
        }

        public async Task<Result<ExpandedEquipmentDto>> GetById(EquipmentId equipmentId)
        {
            var result = await _unitOfWork.EquipmentRepository.GetById(equipmentId);

            return result.IsSuccess ? Result.Ok(_mapper.EquipmentMapper.MapEquipmentToExpandedEquipmentDto(result.Value)) :
                                        Result.Fail(new EquipmentNotFound(equipmentId));
        }

        public async Task<Result<ExpandedEquipmentDto>> Update(EquipmentId equipmentId, UpdateEquipmentDto updateEquipmentDto)
        {
            var retrieveResult = await _unitOfWork.EquipmentRepository.GetById(equipmentId);

            if (retrieveResult.IsFailed)
            {
                return Result.Fail(new EquipmentNotFound(equipmentId));
            }

            _mapper.EquipmentMapper.MapUpdateEquipmentToEquipment(updateEquipmentDto, retrieveResult.Value);
            var updateResult = _unitOfWork.EquipmentRepository.Update(retrieveResult.Value);
            await _unitOfWork.SaveChangesAsync();

            return updateResult.IsSuccess ? Result.Ok(_mapper.EquipmentMapper.MapEquipmentToExpandedEquipmentDto(retrieveResult.Value)) :
                                            Result.Fail(new UpdateError(equipmentId.Value));
        }
    }
}
