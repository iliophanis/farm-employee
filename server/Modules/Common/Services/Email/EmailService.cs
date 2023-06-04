using MailKit.Net.Smtp;
using MimeKit;

namespace server.Modules.Common.Services.Email;
public class EmailService
{
    private readonly EmailConfiguration _emailConfiguration;

    private readonly ILogger<EmailService> _logger;
    public EmailService(ILogger<EmailService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _emailConfiguration = configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
    }

    public async Task SendEmailAsync(EmailMessage emailMessage)
    {
        //create message
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_emailConfiguration.FromMail));
        email.Sender = MailboxAddress.Parse(_emailConfiguration.FromMail);
        var toEmails = emailMessage.ToEmail.Select(x => MailboxAddress.Parse(x)).ToList();
        email.To.AddRange(toEmails);
        email.Subject = emailMessage.Subject;
        var builder = new BodyBuilder();
        if (emailMessage.Attachments != null)
        {
            byte[] fileBytes;
            foreach (var file in emailMessage.Attachments)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                }
            }
        }
        builder.HtmlBody = emailMessage.HtmlBody;
        email.Body = builder.ToMessageBody();
        var smtp = new SmtpClient
        {
            ServerCertificateValidationCallback = (s, c, h, e) => true
        };
        try
        {
            await smtp.ConnectAsync(_emailConfiguration.SmtpHost, _emailConfiguration.SmtpPort, false);
            await smtp.AuthenticateAsync(_emailConfiguration.SmtpUserName, _emailConfiguration.SmtpPassword);
            await smtp.SendAsync(email);
        }
        catch (Exception exc)
        {
            _logger.LogError("Email doesn't send something goes wrong with stmp: " + exc);
        }
        finally
        {
            await smtp.DisconnectAsync(true);
            smtp.Dispose();
        }
    }
}
