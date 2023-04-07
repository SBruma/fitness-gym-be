using FitnessGym.Domain.Entities.Gyms;

namespace FitnessGym.Application.Dtos.Gyms
{
    public class CreateGymDto
    {
        public string Name { get; set; }
        public GeoCoordinate GeoCoordinate { get; set; }
        public Address Address { get; set; }
        public Layout Layout { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
