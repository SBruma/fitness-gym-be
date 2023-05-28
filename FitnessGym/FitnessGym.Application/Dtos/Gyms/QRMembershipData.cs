namespace FitnessGym.Application.Dtos.Gyms
{
    public record QRMembershipData
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}
