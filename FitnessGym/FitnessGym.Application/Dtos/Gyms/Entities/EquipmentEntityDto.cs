using FitnessGym.Domain.Entities.Enums;
using FitnessGym.Domain.Entities.Gyms;

namespace FitnessGym.Application.Dtos.Gyms.Entities
{
    public abstract class EquipmentEntityDto
    {
        public virtual Guid Id { get; set; }
        public string Name { get; set; }
        public string ModelNumber { get; set; }
        public string SerialNumber { get; set; }
        public string? Description { get; set; }
        public DateOnly PurchaseDate { get; set; }
        public DateOnly WarrantyExpirationDate { get; set; }
        public FloorLocation FloorLocation { get; set; }
        public EquipmentCategory Category { get; set; }
        public EquipmentStatus Status { get; set; }
        public Guid GymId { get; set; }
        public int? Floor { get; set; }
    }
}
