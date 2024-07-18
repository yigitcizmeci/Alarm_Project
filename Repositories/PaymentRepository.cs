using System.Security.Claims;
using Alarm_Project.DTOs;
using Alarm_Project.Models;
using Alarm_Project.Repositories.Contracts;
using Alarm_Project.Repositories.DbRepo;
using Alarm_Project.Services;
using Alarm_Project.Services.Contracts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Alarm_Project.Repositories;

public class PaymentRepository(
    RepositoryContext repositoryContext,
    Products products,
    IHttpContextAccessor httpContextAccessor,
    Users users,
    Payment payment,
    IAlarmService alarmService,
    AlarmSettings alarmSettings,
    IEmailSender emailSender,
    AlarmRepository alarmRepository,
    AlarmDto alarmDto,
    IMapper mapper,
    Alarm alarm) : IPaymentRepository<Payment>
{
    private readonly RepositoryContext _repositoryContext = repositoryContext;
    private readonly IAlarmService _alarmService = alarmService;

    public async Task<Payment> AddCard(Payment payment)
    {
        var userIdClaim = httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
        var userId = userIdClaim?.FindFirst(ClaimTypes.NameIdentifier);
        var paymentUserId = new Guid(userId.Value);
        payment.UserId = paymentUserId;

        await _repositoryContext.AddAsync(payment);
        await _repositoryContext.SaveChangesAsync();
        return payment;
    }

    public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
    {
        var AllPayments = await _repositoryContext.Payments.ToListAsync();
        if (AllPayments.Count == 0)
        {
            throw new ArgumentNullException($"There is no any payments");
        }

        return AllPayments.ToList();
    }

    public Task<Payment> GetPaymentByIdAsync()
    {
        throw new Exception();
    }

    public async Task<apiResponse<IdPaymentDto>> MakePaymentIdAsync(IdPaymentDto ıdPaymentDto)
    {
        var identity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
        var UserIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier).Value;

        var existCard =
            await repositoryContext.Payments.FirstOrDefaultAsync(q => q.PaymentId == ıdPaymentDto.PaymentId);
        var existProduct =
            await repositoryContext.Products.FirstOrDefaultAsync(q => q.ProductId == ıdPaymentDto.ProductId);

        if (existCard == null || existProduct == null)
        {
            if (existCard == null)
            {
                throw new ArgumentNullException("Card not found");
            }

            throw new ArgumentNullException("Product not found");
        }

        ıdPaymentDto.UserId = new Guid(UserIdClaim);
        var finalCurrency = existCard.Currency - existProduct.ProductPrice;
        
        //Alarm
        if (finalCurrency < 0)
        {
            var alarmMessage =
                $"Low Currency \n Card Currency: {existCard.Currency} \n Product Price: {existProduct.ProductPrice}";
            var alarmDto = new AlarmDto()
            {
                AlarmType = "Payment Fail",
                AlarmMessage = alarmMessage
            };
            await alarmService.Alarm(alarmDto);
            
            //Slack Message
            if (ıdPaymentDto.AlarmSettingsDto.ReceiveMail == true)
            {
                await alarmService.SendEmailAsync(alarmMessage);
            }
            //Email Message
            if (ıdPaymentDto.AlarmSettingsDto.ReceiveSlack == true)
            {
                await alarmService.SendSlackAsync(alarmMessage);
            }

            return new apiResponse<IdPaymentDto>(
                false, "Low Currency", null);
        }

        existCard.Currency = finalCurrency;
        await repositoryContext.SaveChangesAsync();
        return new apiResponse<IdPaymentDto>(true, "Successful", ıdPaymentDto);
    }

    public async Task<bool> DeleteAllPayments()
    {
        var AllPayments = await _repositoryContext.Payments.ToListAsync();
        repositoryContext.RemoveRange(AllPayments);
        await repositoryContext.SaveChangesAsync();
        return true;
    }

    public async Task<MakePayment> MakePaymentsAsync(MakePayment makePayment)
    {
        try
        {
            var identity = httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                throw new Exception("ClaimsIdentity is null");
            }

            var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new Exception("UserIdClaim is null");
            }

            var userMailClaim = identity.FindFirst(ClaimTypes.Email);
            if (userMailClaim == null)
            {
                throw new Exception("userMailClaim is null");
            }

            var userTokenId = new Guid(userIdClaim.Value);
            var userEmail = userMailClaim.Value;

            if (userTokenId == Guid.Empty)
            {
                throw new Exception("Token ID is invalid");
            }

            // Debugging logs
            Console.WriteLine($"UserTokenId: {userTokenId}");
            Console.WriteLine($"UserEmail: {userEmail}");

            makePayment.UserId = userTokenId;
            makePayment.Payment.UserId = userTokenId;
            makePayment.Products.UserId = userTokenId;
            makePayment.AlarmSettings.UserId = userTokenId;
            makePayment.Alarm.UserId = userTokenId;

            var existUser = await repositoryContext.Users.FindAsync(makePayment.UserId);
            if (existUser == null)
            {
                throw new InvalidOperationException("UserID ile token daki ID eşitlenmedi");
            }

            if (makePayment.Payment.Currency < makePayment.Products.ProductPrice)
            {
                // makePayment.Alarm = new Alarm
                // {
                //     UserId = userTokenId,
                //     AlarmId = Guid.NewGuid(),
                //     AlarmType = "Payment Error",
                //     Time = DateTime.UtcNow,
                //     AlarmMessage = "Currency Exception",
                //     AlarmSettings = makePayment.AlarmSettings
                // };

                await repositoryContext.Alarm.AddAsync(makePayment.Alarm);
                await emailSender.SendEmailAsync(userEmail, "Alarm", "Currency Exception");
                await repositoryContext.SaveChangesAsync();

                throw new Exception($"Payment failed due to insufficient currency for user {userTokenId}");
            }

            makePayment.Payment.Currency -= makePayment.Products.ProductPrice;

            await repositoryContext.Payments.AddAsync(makePayment.Payment);
            await repositoryContext.Products.AddAsync(makePayment.Products);
            await repositoryContext.SaveChangesAsync();

            return makePayment;
        }
        catch (Exception ex)
        {
            // Detailed error logging
            Console.WriteLine($"Error in MakePaymentsAsync: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }

            throw;
        }
    }
}