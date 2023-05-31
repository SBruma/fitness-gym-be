namespace FitnessGym.Application.Dtos.Gyms.Entities
{
    public abstract class MembershipEntityDto
    {
        public virtual Guid Id { get; set; }
        public DateTime RenewalDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public virtual Guid MemberId { get; set; }
        public virtual Guid GymId { get; set; }
        public virtual string? QRCode { get; set; }
    }
}
