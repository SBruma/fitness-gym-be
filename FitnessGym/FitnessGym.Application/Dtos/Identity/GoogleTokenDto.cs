namespace FitnessGym.Application.Dtos.Identity
{
    public class GoogleTokenDto
    {
        public string AccessToken { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
