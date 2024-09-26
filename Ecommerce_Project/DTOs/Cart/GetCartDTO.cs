using Ecommerce_Project.Models;

namespace Ecommerce_Project.DTOs.Cart
{
    public class GetCartDTO
    {
        public int Id { get; set; }
        public long Quantity { get; set; }
        public decimal Total { get; set; }

        // Foreign keys
        public ApplicationUser ApplicationUser { get; set; }
        public Models.Product Product { get; set; }
    }
}
