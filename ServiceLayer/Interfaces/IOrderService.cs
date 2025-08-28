
using DataLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<Order> AddOrder(OrderItem[] items);
        Task<Order> UpdateOrderItems(int orderId, OrderItem[] items);
        Task<Order>  UpdateOrderStatus(int orderId, OrderStatus status);
        Task<Order> UpdateOrder(Order order);
        Task DeleteOrder(int id);

    }
}
