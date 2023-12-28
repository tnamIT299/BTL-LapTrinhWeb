using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;
using Client_Home.Areas.Admin.Services.SendEmail;

namespace Client_Home.Areas.Admin.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public void SendEmail(string toName, string toMail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderAddress));
            message.To.Add(new MailboxAddress(toName, toMail));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body,
            };

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(_smtpSettings.SmtpServer, _smtpSettings.Port, SecureSocketOptions.StartTls);
                client.Authenticate(_smtpSettings.UserName, _smtpSettings.Password);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
