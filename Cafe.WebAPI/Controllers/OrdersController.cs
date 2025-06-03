
using Cafe.Application.DTOs.Orders;
using Cafe.Application.Interfaces.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(OrderCreateDto orderCreateDto)
        {
            var result = await _orderService.Add(orderCreateDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] OrderUpdateDto orderUpdateDto)
        {
            var result = await _orderService.Update(orderUpdateDto);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpDelete("delete/{orderId}")]
        public async Task<IActionResult> Delete(int orderId)
        {
            var result = await _orderService.DeleteOrderAsync(orderId);
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderService.GetAllAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Data);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _orderService.GetById(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result);
        }
     

    }
}
