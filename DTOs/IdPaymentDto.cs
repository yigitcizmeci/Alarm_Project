using Alarm_Project.Models;

namespace Alarm_Project.DTOs;

public record IdPaymentDto
{
    public Guid UserId { get; set; }
    public Guid PaymentId { get; set; }
    public Guid ProductId { get; set; }
    public AlarmSettingsDto AlarmSettingsDto { get; set; }
}