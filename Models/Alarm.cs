namespace Alarm_Project.Models;

public class Alarm
{
    public Guid UserId { get; set; }
    public Guid AlarmId { get; set; } = Guid.NewGuid();
    public string AlarmType { get; set; }
    public DateTime Time { get; set; }
    public string AlarmMessage { get; set; }
    public AlarmSettings AlarmSettings { get; set; }
        
    public Users User { get; set; }
}
