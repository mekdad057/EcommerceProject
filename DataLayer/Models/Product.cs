using DataLayer.Validation;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string? Description { get; set; }

        [Required]
        [Positive]
        public decimal Price { get; set; }

        public ICollection<OrderItem> Orders { get; set; } = new List<OrderItem>();
    }
}
