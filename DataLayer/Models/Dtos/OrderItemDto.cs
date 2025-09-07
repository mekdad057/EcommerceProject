using DataLayer.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Dtos
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        [Required]

        public int ProductId { get; set; }
        public int OrderId { get; set; }
        [Required]
        [Positive]
        public int Quantity { get; set; }
        public ProductDto Product { get; set; }
    }
}
