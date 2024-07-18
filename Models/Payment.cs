using Microsoft.VisualBasic;

namespace Alarm_Project.Models;

public class Payment
{
    public Guid PaymentId { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string CardOwnerName { get; set; }
    public string CardNumber { get; set; }
    public string ExpireYear { get; set; }
    public string CVV { get; set; }
    public int Currency { get; set; }
    public Users Users { get; set; }
    
}
