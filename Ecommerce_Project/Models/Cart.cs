namespace Ecommerce_Project.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public long Quantity { get; set; }
        public decimal Total { get; set; }

        // Foreign keys
        public ApplicationUser ApplicationUser { get; set; }
        public Product Product { get; set; }

    }
}
