using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Update;
using FitnessGym.Application.Errors;
using FitnessGym.Application.Mappers;
using FitnessGym.Application.Services.Interfaces.Gyms;
using FitnessGym.Domain.Entities.Members;
using FitnessGym.Infrastructure.Data.Interfaces;
using FluentResults;

namespace FitnessGym.Application.Services.Gyms
{
    public class StaffScheduleService : IStaffScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StaffScheduleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<StaffScheduleDto>> CreateSchedule(StaffScheduleDto staffScheduleDto)
        {
            var staffSchedules = _mapper.StaffScheduleMapper.MapDtoToEntities(staffScheduleDto);
            await _unitOfWork.StaffScheduleRepository.AddRange(staffSchedules);
            var insertResult = await _unitOfWork.SaveChangesAsync();

            return insertResult.IsSuccess ? Result.Ok(_mapper.StaffScheduleMapper.MapEntitiesToDto(staffSchedules)) :
                                            Result.Fail(new NotCreatedError(typeof(List<StaffSchedule>)));
        }

        public async Task<Result<StaffScheduleDto>> GetStaffSchedule(Guid staffId)
        {
            var staffSchedulesResult = await _unitOfWork.StaffScheduleRepository.GetStaffSchedule(staffId);

            return Result.Ok(_mapper.StaffScheduleMapper.MapEntitiesToDto(staffSchedulesResult.Value));
        }

        public async Task<Result> UpdateSchedule(Guid staffId, UpdateStaffSchedule dto)
        {
            var staffSchedulesResult = await _unitOfWork.StaffScheduleRepository.GetStaffSchedule(staffId);
            _mapper.StaffScheduleMapper.MapUpdateEntities(dto, staffSchedulesResult.Value);

            staffSchedulesResult.Value.ForEach(schedule => _unitOfWork.StaffScheduleRepository.Update(schedule));
            var updateResult = await _unitOfWork.SaveChangesAsync();

            return updateResult.IsSuccess ? Result.Ok() : Result.Fail(new UpdateError(typeof(StaffSchedule)));
        }
    }
}
