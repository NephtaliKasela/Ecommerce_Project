namespace Ecommerce_Project.Models
{
    public class StoreLocation
    {
        public int Id { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }

        // Foreign key
        public Store Store { get; set; }
    }
}
