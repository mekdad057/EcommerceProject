
using DataLayer.Models;
using ServiceLayer.ParameterObjects;

namespace ServiceLayer.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrders(int clientId, PaginationParams paginationParams);
        Task<Order> GetOrderById(int id);
        Task<Order> AddOrder(int clientId, OrderItem[] items);
        Task<Order> UpdateOrderItems(int orderId, OrderItem[] items);
        Task<Order>  UpdateOrderStatus(int orderId, OrderStatus status);
        Task DeleteOrder(int id);

    }
}
