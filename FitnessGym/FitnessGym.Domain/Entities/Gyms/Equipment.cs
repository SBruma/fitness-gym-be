using FitnessGym.Domain.Entities.Enums;
using FitnessGym.Domain.Entities.Interfaces;

namespace FitnessGym.Domain.Entities.Gyms
{
    public class Equipment : IAuditableEntity
    {
        public EquipmentId Id { get; set; }
        public string Name { get; set; }
        public string ModelNumber { get; set; }
        public string SerialNumber { get; set; }
        public string? Description { get; set; }
        public DateOnly PurchaseDate { get; set; }
        public DateOnly WarrantyExpirationDate { get; set; }
        public FloorLocation FloorLocation { get; set; }
        public EquipmentCategory Category { get; set; }
        public EquipmentStatus Status { get; set; }
        public GymId GymId { get; set; }
        public int Level { get; set; }
        public Floor Floor { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public List<MaintenanceHistory> MaintenanceHistory { get; } = new();
    }

    public record struct EquipmentId(Guid Value);
}
