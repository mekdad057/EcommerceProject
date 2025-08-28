using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(await _orderService.GetAllOrders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderService.GetOrderById(id);
            return Ok(order);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateOrder(OrderItem[] items)
        {
            var order = await _orderService.AddOrder(items);
            return Ok(order);
        }

        [HttpPut("{id}/items")]
        public async Task<IActionResult> UpdateOrderItems([FromRoute(Name = "id")] int orderId, OrderItem[] items)
        {
            var order = await _orderService.UpdateOrderItems(orderId, items);
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrder(id);
            return Ok();
        }               
    }
}
