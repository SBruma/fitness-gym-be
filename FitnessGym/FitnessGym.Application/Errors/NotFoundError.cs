using FluentResults;
using System.Net;

namespace FitnessGym.Application.Errors
{
    public class NotFoundError : Error
    {
        public NotFoundError(string message) : base(message)
        {
            Metadata.Add("ErrorCode", HttpStatusCode.NotFound);
        }

        public NotFoundError(Type entity) : base($"{entity.Name} couldn't be found")
        {
            Metadata.Add("ErrorCode", HttpStatusCode.NotFound);
        }
    }
}
