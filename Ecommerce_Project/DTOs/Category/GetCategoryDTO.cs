using Ecommerce_Project.Models;

namespace Ecommerce_Project.DTOs.Category
{
    public class GetCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // Other properties specific to the category

        public List<Models.Subcategory> SubCategories { get; set; }
    }
}
