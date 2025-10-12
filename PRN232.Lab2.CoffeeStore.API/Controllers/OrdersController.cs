using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PRN232.Lab2.CoffeeStore.Services.Interfaces;
using PRN232.Lab2.CoffeeStore.Services.Models.Requests.Order;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace PRN232.Lab2.CoffeeStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaging([FromQuery] OrderPagingRequestModel request)
        {
            var result = await _orderService.GetPagingAsync(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound(new { message = "Không tìm thấy đơn hàng." });
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Lấy userId từ JWT token
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { message = "Không thể xác định người dùng." });
                }

                var result = await _orderService.CreateAsync(request, userId);
                return CreatedAtAction(nameof(GetById), new { id = result.OrderId }, result);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi tạo đơn hàng." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CreateOrderRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Lấy userId từ JWT token
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { message = "Không thể xác định người dùng." });
                }

                var result = await _orderService.UpdateAsync(id, request, userId);
                return Ok(result);
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
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi cập nhật đơn hàng." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _orderService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "Không tìm thấy đơn hàng." });
                }
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi xóa đơn hàng." });
            }
        }
    }
}