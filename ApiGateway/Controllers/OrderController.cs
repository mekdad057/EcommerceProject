using AutoMapper;
using DataLayer.Models;
using DataLayer.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using ServiceLayer.ParameterObjects;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetAllOrders(int clientId, [FromQuery] PaginationParams paginationParams)
        {
            return Ok(await _orderService.GetAllOrders(clientId, paginationParams));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            Order order = await _orderService.GetOrderById(id);
            return Ok(order);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateOrder(int clientId, OrderItemDto[] items)
        {
            OrderItem[] orderItems = _mapper.Map<OrderItem[]>(items);
            var order = await _orderService.AddOrder(clientId, orderItems);
            return Ok(order);
        }

        [HttpPut("{id}/items")]
        public async Task<IActionResult> UpdateOrderItems([FromRoute(Name = "id")] int orderId, OrderItem[] items)
        {
            Order order = await _orderService.UpdateOrderItems(orderId, items);
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
