namespace Alarm_Project.Models;

public class AlarmSettings
{
    public Guid AlarmId { get; set; }
    public bool ReceiveReport { get; set; } = false;
    public bool ReceiveEmail { get; set; } = false;
    public bool ReceiveSlack { get; set; } = false;
    public Guid UserId { get; set; }
    public Alarm Alarm { get; set; }
    public Users Users { get; set; }
}