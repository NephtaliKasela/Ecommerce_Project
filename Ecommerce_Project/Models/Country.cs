namespace Ecommerce_Project.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Freign Keys
        public Continent Continent { get; set; }
        public List<City>? Cities { get; set; }
        public List<Product>? BodyProducts {  get; set; }    
        public List<Store>? Stores {  get; set; }    
        public List<Order>? Orders {  get; set; }    
    }
}
