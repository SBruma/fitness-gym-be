using FitnessGym.Application.Dtos.Identity;
using FitnessGym.Application.Options;
using FitnessGym.Application.Services.Interfaces.Others;
using FluentResults;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

namespace FitnessGym.Application.Services.Others
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;

        public EmailService(IOptionsSnapshot<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public Result SendEmail(MailData mailData)
        {
            try
            {
                using (MimeMessage emailMessage = new MimeMessage())
                {
                    MailboxAddress emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
                    emailMessage.From.Add(emailFrom);
                    MailboxAddress emailTo = new MailboxAddress(mailData.EmailToName, mailData.EmailToId);
                    emailMessage.To.Add(emailTo);
                    emailMessage.Subject = mailData.EmailSubject;

                    BodyBuilder emailBodyBuilder = new BodyBuilder();
                    emailBodyBuilder.HtmlBody = mailData.EmailBody;

                    emailMessage.Body = emailBodyBuilder.ToMessageBody();

                    using (SmtpClient mailClient = new SmtpClient())
                    {
                        mailClient.Connect(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                        mailClient.Authenticate(_mailSettings.UserName, _mailSettings.Password);
                        mailClient.Send(emailMessage);
                        mailClient.Disconnect(true);
                    }
                }

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.InnerException.Message);
            }
        }
    }
}
