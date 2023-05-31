using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Application.Dtos.Gyms.Update;
using FitnessGym.Domain.Entities.Gyms;
using Riok.Mapperly.Abstractions;

namespace FitnessGym.Application.Mappers.Gyms
{
    [Mapper]
    public partial class EquipmentMaintenanceMapper
    {
        private partial MaintenanceHistory CreateDtoToEntity(CreateEquipmentMaintenanceDto dto);
        private partial EquipmentMaintenanceDto EntityToDto(MaintenanceHistory entity);
        [MapperIgnoreSource(nameof(UpdateEquipmentMaintenanceDto.Id))]
        [MapperIgnoreSource(nameof(UpdateEquipmentMaintenanceDto.EquipmentId))]
        private partial void UpdateDtoToEntity(UpdateEquipmentMaintenanceDto dto, MaintenanceHistory entity);

        public MaintenanceHistory MapCreateDtoToEntity(CreateEquipmentMaintenanceDto dto)
        {
            return CreateDtoToEntity(dto);
        }

        public EquipmentMaintenanceDto MapEntityToDto(MaintenanceHistory entity)
        {
            var dto = EntityToDto(entity);
            dto.Id = entity.Id.Value;
            dto.EquipmentId = entity.EquipmentId.Value;

            return dto;
        }

        public List<EquipmentMaintenanceDto> MapEntitiesToDtos(List<MaintenanceHistory> dtos)
        {
            return dtos.Select(MapEntityToDto).ToList();
        }

        public void MapUpdateDtoToEntity(UpdateEquipmentMaintenanceDto dto, MaintenanceHistory entity)
        {
            UpdateDtoToEntity(dto, entity);
        }
    }
}
