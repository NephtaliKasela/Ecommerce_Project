using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Project.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Foreign Keys
        public List<Subcategory>? SubCategories { get; set; }
    }
}
