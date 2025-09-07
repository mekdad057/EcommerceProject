using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
