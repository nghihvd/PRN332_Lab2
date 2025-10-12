using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PRN232.Lab2.CoffeeStore.Services.Interfaces;
using PRN232.Lab2.CoffeeStore.Services.Models.Requests.Payment;

namespace PRN232.Lab2.CoffeeStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaging([FromQuery] PaymentPagingRequestModel request)
        {
            var result = await _paymentService.GetPagingAsync(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var payment = await _paymentService.GetByIdAsync(id);
            if (payment == null)
            {
                return NotFound(new { message = "Không tìm thấy thanh toán." });
            }
            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _paymentService.CreateAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = result.PaymentId }, result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi tạo thanh toán." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdatePaymentRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _paymentService.UpdateAsync(id, request);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi cập nhật thanh toán." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _paymentService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "Không tìm thấy thanh toán." });
                }
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi xóa thanh toán." });
            }
        }
    }
}


