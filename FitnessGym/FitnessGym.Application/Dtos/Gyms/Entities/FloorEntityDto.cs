using FitnessGym.Domain.Entities.Gyms;

namespace FitnessGym.Application.Dtos.Gyms.Entities
{
    public abstract class FloorEntityDto
    {
        public virtual GymId GymId { get; set; }
        public virtual int Floor { get; set; }
    }
}
