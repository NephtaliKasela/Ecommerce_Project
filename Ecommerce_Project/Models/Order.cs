namespace Ecommerce_Project.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderID { get; set; }
        public decimal Total { get; set; }

        // Foreign keys
        public ApplicationUser ApplicationUser { get; set; }
        public Product Product { get; set; }
    }
}
