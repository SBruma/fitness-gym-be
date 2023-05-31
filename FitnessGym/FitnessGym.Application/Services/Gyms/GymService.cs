using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Update;
using FitnessGym.Application.Errors.Gyms;
using FitnessGym.Application.Mappers;
using FitnessGym.Application.Services.Interfaces.Gyms;
using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Infrastructure.Data.Interfaces;
using FluentResults;

namespace FitnessGym.Application.Services.Gyms
{
    public class GymService : IGymService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GymService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GymDto>> Create(CreateGymDto createGymDto)
        {
            var gym = _mapper.GymMapper.CreateGymToGym(createGymDto);
            var addGymResult = await _unitOfWork.GymRepository.Add(gym);
            gym.Floors.AddRange(createGymDto.Floors.Select(floor => new Floor { GymId = gym.Id, Level = floor.Floor }));
            await _unitOfWork.SaveChangesAsync();
            var result = Result.OkIf(addGymResult.IsSuccess, new GymNotCreatedError());

            return result.IsSuccess ? Result.Ok(_mapper.GymMapper.MapGymToGymDto(gym)) : result;
        }

        public async Task<Result> Delete(GymId gymId)
        {
            var getGymResult = await _unitOfWork.GymRepository.GetById(gymId);
            var result = Result.OkIf(getGymResult.IsSuccess, new GymNotFoundError(gymId));

            if (result.IsSuccess)
            {
                _unitOfWork.GymRepository.Delete(getGymResult.Value);
                await _unitOfWork.SaveChangesAsync();
            }

            return result;
        }

        public async Task<Result<ExpandedGymDto>> Get(GymId gymId)
        {
            var getGymResult = await _unitOfWork.GymRepository.GetById(gymId);
            var result = Result.OkIf(getGymResult.IsSuccess, new GymNotFoundError(gymId));

            return result.IsSuccess
                ? _mapper.GymMapper.MapGymToExpandedGymDto(getGymResult.Value)
                : result;
        }

        public async Task<Result<List<GymDto>>> GetAll()
        {
            var getAllResult = await _unitOfWork.GymRepository.GetAll();

            if (getAllResult.IsSuccess)
            {
                return Result.Ok(_mapper.GymMapper.GymsToGymsDto(getAllResult.Value));
            }

            return Result.Ok(new List<GymDto>());
        }

        public async Task<Result<GymDto>> Update(GymId gymToUpdateId, UpdateDetailsGymDto updateGymDto)
        {
            var getGymResult = await _unitOfWork.GymRepository.GetById(gymToUpdateId);
            var result = Result.OkIf(getGymResult.IsSuccess, new GymNotFoundError(gymToUpdateId));

            if (result.IsFailed)
            {
                return result;
            }

            _mapper.GymMapper.UpdateGymToGym(updateGymDto, getGymResult.Value);
            _unitOfWork.GymRepository.Update(getGymResult.Value);
            await _unitOfWork.SaveChangesAsync();
            var gymDto = _mapper.GymMapper.MapGymToGymDto(getGymResult.Value);

            return Result.Ok(gymDto);
        }
    }
}
