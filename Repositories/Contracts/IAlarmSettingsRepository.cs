using Alarm_Project.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Alarm_Project.Repositories.Contracts;

public interface IAlarmSettingsRepository<T>
{
    Task<string> SendReport(string alarmMessage);
    Task<bool> SendEmailAsync(string AlarmMessage);
    Task<bool> SendSlackAsync(string Message);
}