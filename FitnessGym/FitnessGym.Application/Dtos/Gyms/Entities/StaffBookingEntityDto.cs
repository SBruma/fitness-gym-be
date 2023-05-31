using FitnessGym.Domain.Entities.Enums;

namespace FitnessGym.Application.Dtos.Gyms.Entities
{
    public abstract class StaffBookingEntityDto
    {
        public virtual Guid Id { get; set; }
        public Guid StaffId { get; set; }
        public Guid MemberId { get; set; }
        public DateTime SessionStart { get; set; }
        public virtual DateTime SessionEnd { get; set; }
        public BookingRequestStatus RequestStatus { get; set; }
    }
}
