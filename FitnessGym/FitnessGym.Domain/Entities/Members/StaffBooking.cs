using FitnessGym.Domain.Entities.Enums;
using FitnessGym.Domain.Entities.Identity;
using FitnessGym.Domain.Entities.Interfaces;

namespace FitnessGym.Domain.Entities.Members
{
    public class StaffBooking : IAuditableEntity
    {
        public StaffBookingId Id { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime SessionEnd { get; set; }
        public BookingRequestStatus RequestStatus { get; set; }
        public Guid MemberId { get; set; }
        public ApplicationUser Member { get; set; }
        public Guid StaffId { get; set; }
        public ApplicationUser Staff { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
    }

    public record StaffBookingId(Guid Value);
}
