using Alarm_Project.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Alarm_Project.Services.Contracts;

public interface IAlarmService
{
    Task<AlarmDto> Alarm(AlarmDto alarmDto);
    Task<string> SendReport(string alarmMessage);
    Task<bool> SendEmailAsync(string AlarmMessage);
    Task<bool> SendSlackAsync(string message);
}