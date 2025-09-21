using ServiceLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.ParameterObjects;

namespace ServiceLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        private Order CheckOrderExists(Order? order)
        {
            if (order == null)
            {
                throw new ArgumentException("Product Doesn't Exist");
            }
            return order;
        }

        public OrderService(AppDbContext context, UserManager<User> manager)
        {
            _context = context;
            _userManager = manager;
        }

        public async Task<Order> AddOrder(string clientId, OrderItem[] items)
        {
            Order order = new Order();
            order.ClientId = clientId;
            order.CreatedAt = DateTime.Now;
            order.Status = OrderStatus.Processing;
            order.Items = items;
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrders(string clientId, PaginationParams paginationParams)
        {
            int pageSize = paginationParams.PageSize;
            int pageNumber = paginationParams.PageNumber;
            return await _context.Orders.AsNoTracking()
                                        .Where(o => o.ClientId == clientId)
                                        .Skip((pageNumber -1)*pageSize).Take(pageSize)
                                        .ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await _context.Orders.AsNoTracking().AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
            order = CheckOrderExists(order);
            return order;
        }
        

        public async Task DeleteOrder(int id)
        {
            var order = await _context.Orders.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
            order = CheckOrderExists(order);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }


        public async Task<Order> UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateOrderItems(int orderId, OrderItem[] items)
        {
            var order = await GetOrderById(orderId);
            order = CheckOrderExists(order);
            order.Items = items;
            order = await UpdateOrder(order);
            return order;
        }

        public async Task<Order> UpdateOrderStatus(int orderId, OrderStatus new_status)
        {
            var order = await GetOrderById(orderId);
            order.Status = new_status;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        
    }
}
