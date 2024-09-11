namespace Ecommerce_Project.Models
{
    public class Continent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Foreign Keys
        public List<Country>? Countries { get; set; }
    }
}
