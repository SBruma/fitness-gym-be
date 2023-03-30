using FitnessGym.Domain.Entities.Interfaces;

namespace FitnessGym.Domain.Entities.Gyms
{
    public class Floor : IAuditableEntity
    {
        public GymId GymId { get; set; }
        public int Level { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
    }

    public record FloorId(Guid Value);
}
