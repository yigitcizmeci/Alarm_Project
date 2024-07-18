using Alarm_Project.Services.Contracts;
using System.Net.Mail;
using System.Net;

namespace Alarm_Project.Services;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string message, string filePath)
    {
        var mail = "test-sender13@outlook.com";
        var pw = "Test.netapi";

        var client = new SmtpClient("smtp-mail.outlook.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(mail, pw)
        };
        var mailMessage = new MailMessage
        {
            From = new MailAddress(mail),
            To = { email },
            Subject = subject,
            Body = message,
            IsBodyHtml = false
        };
        mailMessage.To.Add(email);
        if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
        {
            var attachment = new Attachment(filePath);
            mailMessage.Attachments.Add(attachment);
        }

        return client.SendMailAsync(mailMessage);
        
        // return client.SendMailAsync(
        //     new MailMessage(from: mail,
        //         to: email,
        //         subject: subject,
        //         body:message));
    }
}