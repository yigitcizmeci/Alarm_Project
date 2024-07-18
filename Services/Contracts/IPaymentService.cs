using Alarm_Project.DTOs;
using Alarm_Project.Models;

namespace Alarm_Project.Services.Contracts;

public interface IPaymentService
{
    Task<UserMakePaymentDto> MakePaymentAsync(UserMakePaymentDto userMakePaymentDto);
    Task<PaymentDto> AddCard(PaymentDto paymentDto);
    Task<IEnumerable<Payment>> GetAllPaymentsAsync();
    Task<apiResponse<IdPaymentDto>> MakePaymentIdAsync(IdPaymentDto ıdPaymentDto);
    Task<Payment> GetPaymentByIdAsync();
}