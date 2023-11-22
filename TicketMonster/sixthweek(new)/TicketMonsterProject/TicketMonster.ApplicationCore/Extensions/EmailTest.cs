using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace TicketMonster.ApplicationCore.Extensions;

public class EmailTest
{
    private readonly string _account;
    private readonly string _password;

    public EmailTest(IConfiguration configuration)
    {
        _account = configuration.GetSection("MailSettings:Account").Value;
        _password = configuration.GetSection("MailSettings:Password").Value;
    }

    public void SendEmailTest(string recipientEmail, string subject, string body)
    {
        using SmtpClient smtpClient = new("smtp.gmail.com", 587);
        smtpClient.Credentials = new NetworkCredential(_account, _password);

        using MailMessage mailMessage = new();
        mailMessage.From = new MailAddress(_account);
        mailMessage.To.Add(recipientEmail);
        mailMessage.Subject = subject;
        mailMessage.Body = body;
        mailMessage.IsBodyHtml = true;
        smtpClient.Send(mailMessage);
    }
}