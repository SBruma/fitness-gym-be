using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Application.Dtos.Gyms.Update;
using FitnessGym.Application.Errors;
using FitnessGym.Application.Mappers;
using FitnessGym.Application.Services.Interfaces.Gyms;
using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Domain.Filters;
using FitnessGym.Infrastructure.Data.Interfaces;
using FluentResults;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

namespace FitnessGym.Application.Services.Gyms
{
    public class EquipmentMaintenanceService : IEquipmentMaintenanceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EquipmentMaintenanceService(IMapper mapper,
                                            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<EquipmentMaintenanceDto>> Create(CreateEquipmentMaintenanceDto maintenanceDto)
        {
            var getActiveMaintenanceResult = await _unitOfWork.MaintenanceHistoryRepository.GetActiveMaintenance(new EquipmentId(maintenanceDto.EquipmentId));

            if (getActiveMaintenanceResult.IsSuccess)
            {
                return Result.Fail(new Error($"There is an active maintenance for {maintenanceDto.EquipmentId}"));
            }

            var entity = _mapper.EquipmentMaintenanceMapper.MapCreateDtoToEntity(maintenanceDto);
            await _unitOfWork.MaintenanceHistoryRepository.Add(entity);
            var createResult = await _unitOfWork.SaveChangesAsync();

            return createResult.IsSuccess ? Result.Ok(_mapper.EquipmentMaintenanceMapper.MapEntityToDto(entity)) :
                                            Result.Fail(new NotCreatedError(typeof(MaintenanceHistory)));
        }

        public async Task<Result> Delete(MaintenanceHistoryId maintenanceHistoryId)
        {
            var getResult = await _unitOfWork.MaintenanceHistoryRepository.GetById(maintenanceHistoryId);

            if (getResult.IsFailed)
            {
                return Result.Fail(new NotFoundError(typeof(MaintenanceHistory)));
            }

            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Result<EquipmentMaintenanceDto>> GetById(MaintenanceHistoryId maintenanceHistoryId)
        {
            var getResult = await _unitOfWork.MaintenanceHistoryRepository.GetById(maintenanceHistoryId);

            return getResult.IsSuccess ? Result.Ok(_mapper.EquipmentMaintenanceMapper.MapEntityToDto(getResult.Value)) :
                                         Result.Fail(new NotFoundError(getResult.Reasons.First().ToString()));
        }

        public async Task<Result<List<EquipmentMaintenanceDto>>> GetFiltered(EquipmentMaintenanceFilter maintenanceFilter, PaginationFilter paginationFilter)
        {
            var getResult = await _unitOfWork.MaintenanceHistoryRepository.GetFiltered(maintenanceFilter, paginationFilter);

            return getResult.IsSuccess ? Result.Ok(_mapper.EquipmentMaintenanceMapper.MapEntitiesToDtos(getResult.Value)) :
                                         Result.Fail(new NotFoundError(typeof(List<MaintenanceHistory>)));
        }

        public async Task<Result<EquipmentMaintenanceDto>> Update(MaintenanceHistoryId maintenanceId, UpdateEquipmentMaintenanceDto updateDto)
        {
            var getResult = await _unitOfWork.MaintenanceHistoryRepository.GetById(maintenanceId);

            if (getResult.IsFailed)
            {
                return Result.Fail(new NotFoundError(typeof(MaintenanceHistory)));
            }

            _mapper.EquipmentMaintenanceMapper.MapUpdateDtoToEntity(updateDto, getResult.Value);
            _unitOfWork.MaintenanceHistoryRepository.Update(getResult.Value);
            var updateResult = await _unitOfWork.SaveChangesAsync();

            return updateResult.IsSuccess ? Result.Ok(_mapper.EquipmentMaintenanceMapper.MapEntityToDto(getResult.Value)) :
                                            Result.Fail(new UpdateError(maintenanceId.Value));
        }
    }
}
