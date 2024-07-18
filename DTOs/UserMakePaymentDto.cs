using Alarm_Project.Models;

namespace Alarm_Project.DTOs;

public record UserMakePaymentDto
{
    public Guid UserId { get; set; }
    public PaymentDto PaymentDto { get; set; }
    public ProductCreateDto ProductCreateDto { get; set; }
    public AlarmSettingsDto AlarmSettingsDto { get; set; }
};