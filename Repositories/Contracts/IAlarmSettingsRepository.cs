using Alarm_Project.Services.Contracts;

namespace Alarm_Project.Repositories.Contracts;

public interface IAlarmSettingsRepository<T>
{
    Task<byte[]> SendReport(string alarmMessage);
    Task<bool> SendEmailAsync(string AlarmMessage);
    Task<bool> SendSlackAsync(string Message);
}