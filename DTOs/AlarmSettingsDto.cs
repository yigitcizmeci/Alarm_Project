namespace Alarm_Project.DTOs;

public record AlarmSettingsDto()
{
    public Guid AlarmId { get; set; }
    public Guid UserId { get; set; }
    public bool ReceiveReport { get; set; }
    public bool ReceiveMail { get; set; }
    public bool ReceiveSlack { get; set; }
}