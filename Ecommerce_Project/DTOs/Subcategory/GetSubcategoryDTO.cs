using Ecommerce_Project.Models;

namespace Ecommerce_Project.DTOs.Subcategory
{
    public class GetSubcategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // Other properties specific to the category

        public Models.Category Category { get; set; } 
        public List<Models.Product>? Products { get; set; }
    }
}
