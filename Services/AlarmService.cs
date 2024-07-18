using Alarm_Project.DTOs;
using Alarm_Project.Models;
using Alarm_Project.Repositories;
using Alarm_Project.Services.Contracts;
using AutoMapper;

namespace Alarm_Project.Services;

public class AlarmService(IMapper mapper, AlarmRepository alarmRepository, AlarmDto alarmDto) : IAlarmService
{
    
    public async Task<AlarmDto> Alarm(AlarmDto alarmDto)
    {
        var map = mapper.Map<Alarm>(alarmDto);
        var mapped =await alarmRepository.Alarm(map);
        var final = mapper.Map<AlarmDto>(mapped);
        return final;
    }

    public async Task<byte[]> SendReport(string alarmMessage)
    {
        return await alarmRepository.SendReport(alarmMessage);
    }

    public async Task<bool> SendEmailAsync(string AlarmMessage)
    {
        return await alarmRepository.SendEmailAsync(AlarmMessage);
    }

    public async Task<bool> SendSlackAsync(string message)
    {
        return await alarmRepository.SendSlackAsync(message);
    }
}