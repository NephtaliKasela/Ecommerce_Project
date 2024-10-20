namespace Ecommerce_Project.Models.Prices
{
    public class ProductPrice
    {
        public int Id { get; set; }
        public long MinimumPieces { get; set; }
        public decimal Price { get; set; }

        // Foreign keys
        public Product BodyProduct { get; set; }
    }
}
