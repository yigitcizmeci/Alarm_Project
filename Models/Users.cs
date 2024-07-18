namespace Alarm_Project.Models;

public class Users
{
    public Guid UserId { get; set; } = Guid.NewGuid();
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }   
    public Payment Payment { get; set; }
    public Products Products { get; set; }
    public AlarmSettings AlarmSettings { get; set; }
}