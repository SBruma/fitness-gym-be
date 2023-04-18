namespace FitnessGym.Application.Errors.Gyms
{
    public class GymNotCreatedError : NotCreatedError
    {
        public GymNotCreatedError() : base($"Gym couldn't be created")
        {

        }
    }
}
