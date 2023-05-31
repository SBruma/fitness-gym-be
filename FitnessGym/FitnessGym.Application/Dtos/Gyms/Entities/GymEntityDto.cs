using FitnessGym.Domain.Entities.Gyms;

namespace FitnessGym.Application.Dtos.Gyms.Entities
{
    public abstract class GymEntityDto
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual GeoCoordinate GeoCoordinate { get; set; }
        public virtual Address Address { get; set; }
        public virtual Layout Layout { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string EmailAddress { get; set; }
    }
}
