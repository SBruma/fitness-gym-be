
namespace FitnessGym.Application.Dtos.Gyms.Create
{
    public class AddMemberDto
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
