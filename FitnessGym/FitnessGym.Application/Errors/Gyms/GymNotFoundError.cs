using FitnessGym.Domain.Entities.Gyms;
using FluentResults;
using System.Net;

namespace FitnessGym.Application.Errors.Gyms
{
    public class GymNotFoundError : Error
    {
        public GymNotFoundError(GymId gymId) 
            : base($"Gym with id {gymId} couldn't be found")
        {
            Metadata.Add("ErrorCode", HttpStatusCode.NotFound);
        }
    }
}
