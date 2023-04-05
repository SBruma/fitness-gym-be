using FitnessGym.Domain.Entities.Interfaces;

namespace FitnessGym.Domain.Entities.Gyms
{
    public class MaintenanceHistory : IAuditableEntity
    {
        public MaintenanceHistoryId Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string? Description { get; set; }
        public decimal? Cost { get; set; }
        public EquipmentId EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
    }

    public record MaintenanceHistoryId(Guid Value);
}
