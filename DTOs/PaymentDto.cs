using Alarm_Project.Models;

namespace Alarm_Project.DTOs;

public record PaymentDto(Guid PaymentId,Guid UserId,string CardOwnerName, string CardNumber, string ExpireYear, string CVV, int Currency);