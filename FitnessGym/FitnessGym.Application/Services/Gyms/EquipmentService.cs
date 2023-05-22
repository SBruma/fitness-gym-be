using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Application.Errors.Gyms;
using FitnessGym.Application.Mappers;
using FitnessGym.Application.Services.Interfaces.Gyms;
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
            var equipment = _mapper.EquipmentMapper.CreateEquipmentToEquipment(createEquipmentDto);
            await _unitOfWork.EquipmentRepository.Add(equipment);
            await _unitOfWork.SaveChangesAsync();
            var result = Result.OkIf(!equipment.Id.Equals(Guid.Empty), new EquipmentNotCreatedError());

            return result.IsSuccess ? Result.Ok(_mapper.EquipmentMapper.MapEquipmentToEquipmentDto(equipment)) : result;
        }

        public async Task<Result<List<EquipmentDto>>> Get(EquipmentFilter equipmentFilter, PaginationFilter paginationFilter)
        {
            var equipments = await _unitOfWork.EquipmentRepository.GetFiltered(equipmentFilter, paginationFilter);

            return Result.Ok(_mapper.EquipmentMapper.EquipmentsToEquipmentsDto(equipments));
        }
    }
}
