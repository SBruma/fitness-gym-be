using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Application.Errors;
using FitnessGym.Application.Mappers;
using FitnessGym.Application.Services.Interfaces.Gyms;
using FitnessGym.Domain.Entities.Members;
using FitnessGym.Domain.Filters;
using FitnessGym.Infrastructure.Data.Interfaces;
using FluentResults;

namespace FitnessGym.Application.Services.Gyms
{
    public class StaffBookingService : IStaffBookingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StaffBookingService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<StaffBookingDto>> Create(CreateStaffBookingDto dto)
        {
            dto.SessionEnd = dto.SessionStart.AddHours(dto.Duration);
            var staffBooking = _mapper.StaffBookingMapper.MapCreateDtoToEntity(dto);
            await _unitOfWork.StaffBookingRepository.Add(staffBooking);
            var createResult = await _unitOfWork.SaveChangesAsync();

            return createResult.IsSuccess ? Result.Ok(_mapper.StaffBookingMapper.MapEntityToDto(staffBooking)) :
                                            Result.Fail(new NotCreatedError(typeof(StaffBooking)));
        }

        public async Task<Result<StaffBookingDto>> GetById(StaffBookingId staffBookingId)
        {
            var staffBookingResult = await _unitOfWork.StaffBookingRepository.GetById(staffBookingId);

            return staffBookingResult.IsSuccess ? Result.Ok(_mapper.StaffBookingMapper.MapEntityToDto(staffBookingResult.Value)) :
                                                    Result.Fail(new NotFoundError(typeof(StaffBooking)));
        }

        public async Task<Result<List<StaffBookingDto>>> GetFitlered(StaffBookingFilter staffBookingFilter)
        {
            var staffBookingResult = await _unitOfWork.StaffBookingRepository.GetStaffBookings(staffBookingFilter);

            return staffBookingResult.IsSuccess ? Result.Ok(_mapper.StaffBookingMapper.MapEntitiesToDtos(staffBookingResult.Value)) :
                                                    Result.Fail(new NotFoundError(typeof(StaffBooking)));
        }
    }
}
