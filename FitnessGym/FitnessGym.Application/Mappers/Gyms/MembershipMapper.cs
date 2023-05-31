using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Domain.Entities.Members;
using Riok.Mapperly.Abstractions;

namespace FitnessGym.Application.Mappers.Gyms
{
    [Mapper]
    public partial class MembershipMapper
    {
        private partial Membership CreateDtoToEntity(CreateMembershipDto dto);
        [MapperIgnoreSource(nameof(Membership.QRCode))]
        private partial MembershipDto EntityToDto(Membership entity);

        public Membership MapCreateDtoToEntity(CreateMembershipDto dto)
        {
            var entity = CreateDtoToEntity(dto);

            return entity;
        }

        public MembershipDto MapEntityToDto(Membership entity)
        {
            var dto = EntityToDto(entity);
            dto.QRCode = Convert.ToBase64String(entity.QRCode.Value);
            dto.MemberId = entity.MemberId;
            dto.GymId = entity.GymId.Value;
            dto.Id = entity.Id.Value;

            return dto;
        }

        public List<MembershipDto> MapEntityToDto(List<Membership> entities)
        {
            var dtos = entities.Select(MapEntityToDto).ToList();

            return dtos;
        }
    }
}
