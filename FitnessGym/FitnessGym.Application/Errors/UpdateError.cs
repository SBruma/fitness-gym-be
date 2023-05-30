using FluentResults;
using System.Net;

namespace FitnessGym.Application.Errors
{
    public class UpdateError : Error
    {
        public UpdateError(Guid id) : base($"Object with id {id} couldn't be updated")
        {
            Metadata.Add("ErrorCode", HttpStatusCode.InternalServerError);
        }

        public UpdateError(Type type) : base($"{type} couldn't be updated")
        {
            Metadata.Add("ErrorCode", HttpStatusCode.InternalServerError);
        }
    }
}
