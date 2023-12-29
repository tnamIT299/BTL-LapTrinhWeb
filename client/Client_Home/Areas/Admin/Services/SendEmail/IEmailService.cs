namespace Client_Home.Areas.Admin.Services.SendEmail
{
    public interface IEmailService
    {
        Task SendMail(MailContent mailContent);
    }
}
