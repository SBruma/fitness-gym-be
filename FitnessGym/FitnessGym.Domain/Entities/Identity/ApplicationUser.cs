using FitnessGym.Domain.Entities.Enums;
using FitnessGym.Domain.Entities.Interfaces;
using FitnessGym.Domain.Entities.Members;
using Microsoft.AspNetCore.Identity;

namespace FitnessGym.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>, IAuditableEntity
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
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public string AccesToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public List<StaffSchedule> StaffSchedule { get; } = new();
        public List<Membership> Memberships { get; } = new();
    }
}
