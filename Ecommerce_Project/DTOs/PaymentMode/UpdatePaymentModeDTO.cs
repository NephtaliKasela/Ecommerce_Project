namespace Ecommerce_Project.DTOs.PaymentMode
{
    public class UpdatePaymentModeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Foreign keys
        public List<Models.Order>? Orders { get; set; }
    }
}
