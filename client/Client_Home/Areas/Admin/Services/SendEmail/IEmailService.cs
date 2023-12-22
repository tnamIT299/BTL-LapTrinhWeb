namespace Client_Home.Areas.Admin.Services.SendEmail
{
    public interface IEmailService
    {
        void SendEmail(string toName, string toMail, string subject, string body);
    }
}
