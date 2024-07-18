using Alarm_Project.DTOs;
using Alarm_Project.Models;
using Alarm_Project.Services;

namespace Alarm_Project.Repositories.Contracts;

public interface IPaymentRepository<T>
{
    Task<T> AddCard(T entity);
    Task<IEnumerable<T>> GetAllPaymentsAsync();
    Task<T> GetPaymentByIdAsync();
    Task<MakePayment> MakePaymentsAsync(MakePayment makePayment);
    Task<apiResponse<IdPaymentDto>> MakePaymentIdAsync(IdPaymentDto ıdPaymentDto);
    Task<bool> DeleteAllPayments();
}