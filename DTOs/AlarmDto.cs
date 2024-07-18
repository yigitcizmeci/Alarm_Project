namespace Alarm_Project.DTOs;

public record AlarmDto()
{
    public Guid UserId { get; set; }
    public Guid AlarmId { get; set; }
    public string AlarmType { get; set; } 
    public string AlarmMessage { get; set; }
    public DateTime Time { get; set; }
}