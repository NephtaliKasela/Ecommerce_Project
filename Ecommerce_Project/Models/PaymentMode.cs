namespace Ecommerce_Project.Models
{
    public class PaymentMode
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Foreign keys
        public List<Order>? Orders { get; set; }
    }
}
