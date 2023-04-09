using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Errors.Gyms;
using FitnessGym.Application.Mapper.Gyms;
using FitnessGym.Application.Services.Interfaces.Gyms;
using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Infrastructure.Data.Interfaces;
using FluentResults;

namespace FitnessGym.Application.Services.Gyms
{
    public class GymService : IGymService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GymMapper _gymMapper;

        public GymService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _gymMapper = new GymMapper();
        }

        public async Task<Result<GymDto>> Create(CreateGymDto createGymDto)
        {
            var gym = _gymMapper.CreateGymToGym(createGymDto);
            await _unitOfWork.GymRepository.Add(gym);
            await _unitOfWork.SaveChangesAsync();
            var result = Result.OkIf(gym.Id is not null, new GymNotCreatedError());

            return result.IsSuccess ? Result.Ok(_gymMapper.MapGymToGymDto(gym)) : result;
        }

        public async Task<Result> Delete(GymId gymId)
        {
            var gym = await _unitOfWork.GymRepository.GetById(gymId);
            var result = Result.OkIf(gym is not null, new GymNotFoundError(gymId));

            if (result.IsSuccess)
            {
                _unitOfWork.GymRepository.Delete(gym);
                await _unitOfWork.SaveChangesAsync();
            }

            return result;
        }

        public async Task<Result<GymDto>> Get(GymId gymId)
        {
            var gym = await _unitOfWork.GymRepository.GetById(gymId);
            var result = Result.OkIf(gym is not null, new GymNotFoundError(gymId));

            return result.IsSuccess
                ? _gymMapper.MapGymToGymDto(gym)
                : result;
        }

        public async Task<Result<List<GymDto>>> GetAll()
        {
            var gyms = await _unitOfWork.GymRepository.GetAll();
            var gymsDto = _gymMapper.GymsToGymsDto(gyms);

            return Result.Ok(gymsDto);
        }

        public async Task<Result<GymDto>> Update(GymId gymToUpdateId, UpdateGymDto updateGymDto)
        {
            var gym = await _unitOfWork.GymRepository.GetById(gymToUpdateId);
            var result = Result.OkIf(gym is not null, new GymNotFoundError(gymToUpdateId));

            if (result.IsFailed)
            {
                return result;
            }

            _gymMapper.UpdateGymToGym(updateGymDto, gym);
            _unitOfWork.GymRepository.Update(gym);
            await _unitOfWork.SaveChangesAsync();
            var gymDto = _gymMapper.MapGymToGymDto(gym);

            return Result.Ok(gymDto);
        }
    }
}
