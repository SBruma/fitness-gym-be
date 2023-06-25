using FitnessGym.Domain.Entities.Interfaces;
using FitnessGym.Domain.Entities.Members;

namespace FitnessGym.Domain.Entities.Gyms
{
    public class GymCheckIn : IAuditableEntity
    {
        public Guid Id { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public MembershipId MembershipId { get; set; }
        public Membership Membership { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
    }
}
