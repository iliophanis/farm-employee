namespace server.Modules.Common.Services.Email;

public class EmailMessage
{
    public string Subject { get; set; }
    public string HtmlBody { get; set; }
    public List<IFormFile> Attachments { get; set; }

    public List<string> ToEmail { get; set; }
}
