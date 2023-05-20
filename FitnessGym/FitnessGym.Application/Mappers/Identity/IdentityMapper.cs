using FitnessGym.Application.Dtos.Identity;
using FitnessGym.Domain.Entities.Identity;
using Riok.Mapperly.Abstractions;

namespace FitnessGym.Application.Mappers.Identity
{
    [Mapper]
    public partial class IdentityMapper
    {
        public partial ApplicationUser RegisterDtoToUser(RegisterDto registerDto);
    }
}
