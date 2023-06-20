using FitnessGym.Domain.Entities.Enums;

namespace FitnessGym.Application.Dtos.Gyms.Update
{
    public class UpdateUserDto
    {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? ProfilePicture { get; set; }
        public Gender? Gender { get; set; }
    }
}
