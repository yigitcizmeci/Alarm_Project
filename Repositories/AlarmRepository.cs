using System.Security.Claims;
using System.Text;
using Alarm_Project.DTOs;
using Alarm_Project.Models;
using Alarm_Project.Repositories.Contracts;
using Alarm_Project.Repositories.DbRepo;
using Alarm_Project.Services;
using Alarm_Project.Services.Contracts;
using AutoMapper;
using Slack.Webhooks;

namespace Alarm_Project.Repositories;

public class AlarmRepository(
    RepositoryContext repositoryContext,
    AlarmSettings alarmSettings,
    Alarm alarm,
    Users users,
    IMapper mapper,
    SlackService slackService,
    IHttpContextAccessor httpContextAccessor,
    IEmailSender emailSender) : IAlarmRepository<Alarm>, IAlarmSettingsRepository<AlarmSettings>
{
    public async Task<Alarm> Alarm(Alarm alarm)
    {
        var Identity = httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
        var UserIdClaim = Identity.FindFirst(ClaimTypes.NameIdentifier).Value;
        alarm = new Alarm()
        {
            UserId = new Guid(UserIdClaim),
            AlarmId = alarm.AlarmId,
            AlarmType = alarm.AlarmType,
            AlarmMessage = alarm.AlarmMessage,
            AlarmSettings = alarm.AlarmSettings,
            Time = DateTime.UtcNow
        };
        var result = await Task.FromResult(alarm);
        await repositoryContext.AddAsync(result);
        await repositoryContext.SaveChangesAsync();
        return result;
    }

    public async Task<byte[]> SendReport(string alarmMessage)
    {
        var reportContent = GenerateReport(alarmMessage);
        var fileBytes = Encoding.UTF8.GetBytes(reportContent);

        await Task.CompletedTask;
        return fileBytes;
    }

    private string GenerateReport(string message)
    {
        var report = new StringBuilder();
        report.AppendLine("Alarm DOC");
        report.AppendLine("==================");
        report.AppendLine($"Mesaj: {message}");
        report.AppendLine($"Tarih: {DateTime.Now}");
        return report.ToString();
    }

    public async Task<bool> SendEmailAsync(string AlarmMessage)
    {
        var EmailClaim = httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
        var userEmail = EmailClaim?.FindFirst(ClaimTypes.Email);
        var userIdClaim = EmailClaim.FindFirst(ClaimTypes.NameIdentifier).Value;

        if (userEmail == null)
        {
            throw new ArgumentNullException("User Email claim in not present");
        }

        var receiver = userEmail.ToString();
        var subject = "Alarm Notification";
        var message = AlarmMessage;

        await emailSender.SendEmailAsync(receiver, subject, message);
        return true;
    }

    public async Task<bool> SendSlackAsync(string message)
    {
        return await slackService.SendSlackMessage(message);
    }
}