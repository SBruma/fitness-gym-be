using FitnessGym.Domain.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;

namespace FitnessGym.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public DateOnly DateOfBirth { get; set; }
        [PersonalData]
        public string? EmergencyPhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }
        [PersonalData]
        public Gender Gender { get; set; } = Gender.Other;
        public DateTime CreationDate { get; set; }
    }
}
