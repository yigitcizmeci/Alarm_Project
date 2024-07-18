using Alarm_Project.DTOs;
using Alarm_Project.Models;

namespace Alarm_Project.Repositories.Contracts;

public interface IAlarmRepository<T>
{
    Task<Alarm> Alarm(Alarm alarm);
}