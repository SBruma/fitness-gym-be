using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Domain.Entities.Members;
using Riok.Mapperly.Abstractions;

namespace FitnessGym.Application.Mappers.Gyms
{
    [Mapper]
    public partial class StaffBookingMapper
    {
        private partial StaffBooking CreateDtoToEntity(CreateStaffBookingDto dto);
        private partial StaffBookingDto EntityToDto(StaffBooking entity);

        public StaffBooking MapCreateDtoToEntity(CreateStaffBookingDto dto) => CreateDtoToEntity(dto);
        public StaffBookingDto MapEntityToDto(StaffBooking entity)
        {
            var dto = EntityToDto(entity);
            dto.Id = entity.Id.Value;

            return dto;
        }

        public List<StaffBookingDto> MapEntitiesToDtos(List<StaffBooking> entities) => entities.Select(MapEntityToDto).ToList();
    }
}
