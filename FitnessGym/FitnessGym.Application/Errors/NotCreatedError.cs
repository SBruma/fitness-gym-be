using FluentResults;
using System.Net;

namespace FitnessGym.Application.Errors
{
    public class NotCreatedError : Error
    {
        public NotCreatedError(string message) : base(message)
        {
            Metadata.Add("ErrorCode", HttpStatusCode.BadRequest);
        }
    }
}
