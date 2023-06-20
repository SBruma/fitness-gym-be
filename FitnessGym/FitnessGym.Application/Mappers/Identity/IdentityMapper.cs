using FitnessGym.Application.Dtos.Gyms.Update;
using FitnessGym.Application.Dtos.Identity;
using FitnessGym.Domain.Entities.Identity;
using Riok.Mapperly.Abstractions;

namespace FitnessGym.Application.Mappers.Identity
{
    [Mapper]
    public partial class IdentityMapper
    {
        public partial ApplicationUser RegisterDtoToUser(RegisterDto registerDto);
        public void Update(UpdateUserDto dto, ApplicationUser applicationUser)
        {
            applicationUser.ProfilePicture = dto.ProfilePicture ?? applicationUser.ProfilePicture;
            applicationUser.FirstName = dto.FirstName ?? applicationUser.FirstName;
            applicationUser.LastName = dto.LastName ?? applicationUser.LastName;
            applicationUser.Gender = dto.Gender ?? applicationUser.Gender;
            applicationUser.DateOfBirth = dto.DateOfBirth ?? applicationUser.DateOfBirth;
        }
    }
}
