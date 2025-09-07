using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Completed,
        Cancelled
    }

    public class Order
    {
        public int Id { get; set; }

        public int ClientId { get; set; }   

        public DateTime CreatedAt { get; set; }

        [BindNever]
        public OrderStatus Status { get; set; } 

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        
        public User Client { get; set; }

        [NotMapped]
        public decimal TotalPrice { 
            get {
                return Items.Sum(item => item.Quantity * item.Product.Price);
            } 
        }

    }
}
