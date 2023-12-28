namespace Client_Home.Areas.Admin.Services.SendEmail
{
    public class SmtpSettings
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
    }

}
