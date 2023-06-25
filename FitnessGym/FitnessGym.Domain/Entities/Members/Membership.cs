using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Domain.Entities.Identity;
using FitnessGym.Domain.Entities.Interfaces;

namespace FitnessGym.Domain.Entities.Members
{
    public class Membership : IAuditableEntity
    {
        public MembershipId Id { get; set; }
        public DateTime RenewalDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public QRCode QRCode { get; set; }
        public Guid MemberId { get; set; }
        public ApplicationUser Member { get; set; }
        public GymId GymId { get; set; }
        public Gym Gym { get; set; }
        public List<GymCheckIn> GymCheckIns { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
    }

    public record struct MembershipId(Guid Value);
    public record struct QRCode(byte[] Value);
}
