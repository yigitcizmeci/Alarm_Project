using Alarm_Project.Models;

namespace Alarm_Project.DTOs;

public record MakePayment
{
    public Guid UserId { get; set; }
    public Payment Payment { get; set; }
    public Products Products { get; set; }
    public AlarmSettings AlarmSettings { get; set; }
    public Alarm Alarm { get; set; }
};