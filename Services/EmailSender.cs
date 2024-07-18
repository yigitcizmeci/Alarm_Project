using Alarm_Project.Services.Contracts;
using System.Net.Mail;
using System.Net;

namespace Alarm_Project.Services;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string message)
    {
        var mail = "test-sender13@outlook.com";
        var pw = "Test.netapi";

        var client = new SmtpClient("smtp-mail.outlook.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(mail, pw)
        };
        return client.SendMailAsync(
            new MailMessage(from: mail,
                to: email,
                subject: subject,
                message));
    }
}