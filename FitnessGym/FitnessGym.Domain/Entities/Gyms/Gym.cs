using FitnessGym.Domain.Entities.Interfaces;
using FitnessGym.Domain.Entities.Members;

namespace FitnessGym.Domain.Entities.Gyms
{
    public class Gym : IAuditableEntity
    {
        public GymId Id { get; set; }
        public string Name { get; set; }
        public GeoCoordinate GeoCoordinate { get; set; }
        public Address Address { get; set; }
        public Layout Layout { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public List<Membership> Memberships { get; set; } = new();
        public List<Floor> Floors { get; set; } = new();
    }

    public record GymId(Guid Value);
}
