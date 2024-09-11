namespace Ecommerce_Project.Models.Prices
{
    public class ProductPrice
    {
        public int Id { get; set; }
        public double MinimumPieces { get; set; }
        public double Price { get; set; }

        // Foreign keys
        public Product BodyProduct { get; set; }
    }
}
