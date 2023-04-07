using FluentResults;
using System.Net;

namespace FitnessGym.Application.Errors.Gyms
{
    public class GymNotCreatedError : Error
    {
        public GymNotCreatedError()
            : base($"Gym couldn't be created")
        {
            Metadata.Add("ErrorCode", HttpStatusCode.BadRequest);
        }
    }
}
