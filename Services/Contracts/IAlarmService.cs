using Alarm_Project.DTOs;

namespace Alarm_Project.Services.Contracts;

public interface IAlarmService
{
    Task<AlarmDto> Alarm(AlarmDto alarmDto);
    Task<byte[]> SendReport(string alarmMessage);
    Task<bool> SendEmailAsync(string AlarmMessage);
    Task<bool> SendSlackAsync(string message);
}