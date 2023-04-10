using FitnessGym.Domain.Entities.Interfaces;

namespace FitnessGym.Domain.Entities.Gyms
{
    public class Floor : IAuditableEntity
    {
        public GymId GymId { get; set; }
        public Gym Gym { get; set; }
        public int Level { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public List<Equipment> Equipments { get; } = new();

        public Floor()
        {

        }

        public Floor(GymId gymId, int level)
        {
            GymId = gymId;
            Level = level;
        }
    }
}
