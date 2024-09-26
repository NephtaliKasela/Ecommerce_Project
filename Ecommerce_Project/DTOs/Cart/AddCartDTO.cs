using Ecommerce_Project.Models;

namespace Ecommerce_Project.DTOs.Cart
{
    public class AddCartDTO
    {
        public long Quantity { get; set; }
        public decimal Total { get; set; }

        // Foreign keys
        public ApplicationUser ApplicationUser { get; set; }
        public string ProductId { get; set; }
    }
}
