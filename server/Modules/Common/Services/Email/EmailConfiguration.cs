namespace server.Modules.Common.Services.Email;
public class EmailConfiguration
{
    public string SmtpHost { get; set; }
    public int SmtpPort { get; set; }
    public string SmtpUserName { get; set; }
    public string SmtpPassword { get; set; }
    public string FromMail { get; set; }
}
