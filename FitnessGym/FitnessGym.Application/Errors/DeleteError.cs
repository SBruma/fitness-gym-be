using FluentResults;
using System.Net;

namespace FitnessGym.Application.Errors
{
    public class DeleteError : Error
    {
        public DeleteError(Guid id) : base($"Object with id {id} couldn't be deleted")
        {
            Metadata.Add("ErrorCode", HttpStatusCode.InternalServerError);
        }
    }
}
