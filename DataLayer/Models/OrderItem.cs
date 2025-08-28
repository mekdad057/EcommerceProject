using DataLayer.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        [Required]
        public required int ProductId { get; set; }

        public required int OrderId { get; set; }    

        public Product Product { get; set; }
        
        public Order Order { get; set; }

        [Required]
        [Positive]
        public int Quantity { get; set; }
    }
}
