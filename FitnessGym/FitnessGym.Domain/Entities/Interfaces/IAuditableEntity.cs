namespace FitnessGym.Domain.Entities.Interfaces
{
    public interface IAuditableEntity
    {
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
    }
}
