using Alarm_Project.DTOs;
using Alarm_Project.Models;
using Alarm_Project.Repositories;
using Alarm_Project.Services.Contracts;
using AutoMapper;

namespace Alarm_Project.Services;

public class PaymentService(PaymentRepository paymentRepository,IMapper mapper) : IPaymentService
{
    private readonly IMapper _mapper = mapper;

    public async Task<UserMakePaymentDto> MakePaymentAsync(UserMakePaymentDto userMakePaymentDto)
    {
        try
        {
            var makePayment = _mapper.Map<MakePayment>(userMakePaymentDto);
            var result = await paymentRepository.MakePaymentsAsync(makePayment);
            var userMakePaymentDtoResult = _mapper.Map<UserMakePaymentDto>(result);
            return userMakePaymentDtoResult;
        }
        catch (Exception ex)
        {
            // Detailed error logging
            Console.WriteLine($"Error in MakePaymentAsync Service: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            throw;
        }
    }


    public async Task<PaymentDto> AddCard(PaymentDto paymentDto)
    {
        var first = _mapper.Map<Payment>(paymentDto);
        var map = await paymentRepository.AddCard(first);
        var last = _mapper.Map<PaymentDto>(map);
        return last;
    }

    public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
    {
        return await paymentRepository.GetAllPaymentsAsync();
    }

    public async Task<apiResponse<IdPaymentDto>> MakePaymentIdAsync(IdPaymentDto ıdPaymentDto)
    {
        return await paymentRepository.MakePaymentIdAsync(ıdPaymentDto);
    }

    public Task<Payment> GetPaymentByIdAsync()
    {
        throw new Exception();
    }
}