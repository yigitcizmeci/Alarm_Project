using Alarm_Project.DTOs;
using Alarm_Project.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alarm_Project.Controllers
{
    [Route("api/Payments")]
    [ApiController]
    public class PaymentController(IPaymentService paymentService) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        [Route("AddCard")]
        public async Task<IActionResult> AddCard(PaymentDto paymentDto)
        {
            return Ok(await paymentService.AddCard(paymentDto));
        }
        
        [HttpPost]
        [Authorize]
        [Route("MakePayment")]
        public async Task<IActionResult> MakePaymentAsync(UserMakePaymentDto userMakePaymentDto)
        {
            try
            {
                var result = await paymentService.MakePaymentAsync(userMakePaymentDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Detailed error logging
                Console.WriteLine($"Error in MakePaymentAsync Controller: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Authorize]
        [Route("EasyPayment")]
        public async Task<IActionResult> MakePaymentIdAsync(IdPaymentDto ıdPaymentDto)
        {
            return Ok(await paymentService.MakePaymentIdAsync(ıdPaymentDto));
        }

        

        [HttpGet]
        [Route("GetAllPayments")]
        public async Task<IActionResult> GetAllPayments()
        {
            return Ok(await paymentService.GetAllPaymentsAsync());
        }
    }
}
