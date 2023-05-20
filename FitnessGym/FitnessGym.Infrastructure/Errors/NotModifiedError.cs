using FluentResults;
using System.Net;

namespace FitnessGym.Infrastructure.Errors
{
    public class NotModifiedError : Error
    {
        public NotModifiedError(Type objectType) : base($"{objectType.Name} couldn't be modified")
        {
            Metadata.Add("ErrorCode", HttpStatusCode.BadRequest);
        }
    }
}
