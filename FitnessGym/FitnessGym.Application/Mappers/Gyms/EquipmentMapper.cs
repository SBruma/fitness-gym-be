using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Application.Dtos.Gyms.Expanded;
using FitnessGym.Application.Dtos.Gyms.Update;
using FitnessGym.Domain.Entities.Gyms;
using Riok.Mapperly.Abstractions;

namespace FitnessGym.Application.Mappers.Gyms
{
    [Mapper]
    public partial class EquipmentMapper
    {
        [MapperIgnoreSource(nameof(CreateEquipmentDto.Floor))]
        private partial Equipment CreateEquipmentToEquipment(CreateEquipmentDto createEquipmentDto);
        [MapperIgnoreSource(nameof(CreateEquipmentDto.Floor))]
        private partial EquipmentDto EquipmentToEquipmentDto(Equipment equipment);
        [MapperIgnoreSource(nameof(CreateEquipmentDto.Floor))]
        private partial ExpandedEquipmentDto EquipmentToExpandedEquipmentDto(Equipment equipment);
        [MapperIgnoreSource(nameof(CreateEquipmentDto.Floor))]
        [MapperIgnoreSource(nameof(UpdateDetailsGymDto.Id))]
        private partial void UpdateEquipmentToEquipment(UpdateEquipmentDto updateEquipmentDto, Equipment equipment);

        public Equipment MapCreateEquipmentToEquipment(CreateEquipmentDto createEquipmentDto)
        {
            var equipment = CreateEquipmentToEquipment(createEquipmentDto);
            equipment.Level = createEquipmentDto.Floor;

            return equipment;
        }

        public List<Equipment> MapCreateEquipmentToEquipment(List<CreateEquipmentDto> dtos)
        {
            return dtos.Select(MapCreateEquipmentToEquipment).ToList();
        }

        public EquipmentDto MapEquipmentToEquipmentDto(Equipment equipment)
        {
            var dto = EquipmentToEquipmentDto(equipment);
            dto.Id = equipment.Id.Value;
            dto.Floor = equipment.Level;
            dto.GymId = equipment.GymId.Value;

            return dto;
        }

        public List<EquipmentDto> EquipmentsToEquipmentsDto(List<Equipment> equipments)
        {
            return equipments.Select(MapEquipmentToEquipmentDto).ToList();
        }

        public ExpandedEquipmentDto MapEquipmentToExpandedEquipmentDto(Equipment equipment)
        {
            var dto = EquipmentToExpandedEquipmentDto(equipment);
            dto.Id = equipment.Id.Value;
            dto.Floor = equipment.Level;
            dto.GymId = equipment.GymId.Value;

            return dto;
        }

        public void MapUpdateEquipmentToEquipment(UpdateEquipmentDto updateEquipmentDto, Equipment equipment)
        {
            UpdateEquipmentToEquipment(updateEquipmentDto, equipment);
            equipment.Level = updateEquipmentDto.Floor;
        }
    }
}
