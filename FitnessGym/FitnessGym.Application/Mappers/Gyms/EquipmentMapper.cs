using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Domain.Entities.Gyms;
using Riok.Mapperly.Abstractions;

namespace FitnessGym.Application.Mappers.Gyms
{
    [Mapper]
    public partial class EquipmentMapper
    {
        [MapperIgnoreSource(nameof(CreateEquipmentDto.Floor))]
        public partial Equipment CreateEquipmentToEquipment(CreateEquipmentDto createEquipmentDto);
        [MapperIgnoreSource(nameof(CreateEquipmentDto.Floor))]
        public partial EquipmentDto EquipmentToEquipmentDto(Equipment equipment);

        public Equipment MapCreateEquipmentToEquipment(CreateEquipmentDto createEquipmentDto)
        {
            var equipment = CreateEquipmentToEquipment(createEquipmentDto);
            equipment.Level = createEquipmentDto.Floor;

            return equipment;
        }

        public EquipmentDto MapEquipmentToEquipmentDto(Equipment equipment)
        {
            var dto = EquipmentToEquipmentDto(equipment);
            dto.Id = equipment.Id;

            return dto;
        }

        public List<EquipmentDto> EquipmentsToEquipmentsDto(List<Equipment> equipments)
        {
            return equipments.Select(MapEquipmentToEquipmentDto).ToList();
        }
    }
}
