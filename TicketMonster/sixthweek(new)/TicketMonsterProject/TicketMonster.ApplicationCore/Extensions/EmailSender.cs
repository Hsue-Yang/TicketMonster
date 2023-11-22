using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;

namespace TicketMonster.ApplicationCore.Extensions;

public class EmailSender
{
    private readonly string _account;
    private readonly string _password;

    public EmailSender(IConfiguration configuration)
    {
        _account = configuration.GetSection("MailSettings:Account").Value;
        _password = configuration.GetSection("MailSettings:Password").Value;
    }

    public Task SendEmail(string recipientName, string recipientEmail, string subject, string htmlBody)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("TicketMonster", "haoyun0111@gmail.com"));
            message.To.Add(new MailboxAddress(recipientName, recipientEmail));
            message.Subject = subject;
            var bodyBuilder = new BodyBuilder { HtmlBody = htmlBody };
            message.Body = bodyBuilder.ToMessageBody();

            var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate(_account, _password);
            client.Send(message);
            client.Disconnect(true);
        }
        catch (Exception){}
        return Task.CompletedTask;
    }
}
