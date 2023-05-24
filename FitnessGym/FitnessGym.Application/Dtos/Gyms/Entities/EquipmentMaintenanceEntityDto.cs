using FitnessGym.Domain.Entities.Gyms;

namespace FitnessGym.Application.Dtos.Gyms.Entities
{
    public abstract class EquipmentMaintenanceEntityDto
    {
        public virtual Guid Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Description { get; set; }
        public decimal? Cost { get; set; }
        public virtual Guid EquipmentId { get; set; }
    }
}
