using FitnessGym.Application.Dtos.Identity;
using FluentResults;

namespace FitnessGym.Application.Services.Interfaces.Others
{
    public interface IEmailService
    {
        public Result SendEmail(MailData maildata);
    }
}
