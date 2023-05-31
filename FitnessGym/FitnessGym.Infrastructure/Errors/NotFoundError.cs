using FluentResults;
using System.Net;

namespace FitnessGym.Infrastructure.Errors
{
    public class NotFoundError : Error
    {
        public NotFoundError(Type objectType) : base($"{objectType.Name} couldn't be found")
        {
            Metadata.Add("ErrorCode", HttpStatusCode.NotFound);
        }
    }
}
