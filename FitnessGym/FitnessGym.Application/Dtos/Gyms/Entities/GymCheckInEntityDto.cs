namespace FitnessGym.Application.Dtos.Gyms.Entities
{
    public abstract class GymCheckInEntityDto
    {
        public virtual Guid Id { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
    }
}
