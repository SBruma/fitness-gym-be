using FluentResults;
using System.Net;

namespace FitnessGym.Infrastructure.Errors
{
    public class NotCreatedError : Error
    {
        public NotCreatedError(Type objectType) : base($"{objectType.Name} couldn't be created")
        {
            Metadata.Add("ErrorCode", HttpStatusCode.BadRequest);
        }
    }
}
