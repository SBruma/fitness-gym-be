namespace FitnessGym.Application.Dtos.Gyms.Create
{
    public class CreateMembershipDto
    {
        public string Email { get; set; }
        public Guid GymId { get; set; }
        public DateTime RenewalDate { get; set; }
        public int Months { get; set; }
    }
}
