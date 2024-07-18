namespace Alarm_Project.Services.Contracts;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message, string filePath);
}