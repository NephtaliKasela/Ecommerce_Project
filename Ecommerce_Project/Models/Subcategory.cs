namespace Ecommerce_Project.Models
{
    public class Subcategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Foreign Keys
        public Category Category { get; set; }
        public List<Product>? Products { get; set; }

    }
}
