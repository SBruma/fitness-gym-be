using FitnessGym.Domain.Entities.Gyms;

namespace FitnessGym.Application.Errors.Gyms
{
    public class GymNotFoundError : NotFoundError
    {
        public GymNotFoundError(GymId gymId) : base($"Gym with id {gymId} couldn't be found")
        {

        }
    }
}
