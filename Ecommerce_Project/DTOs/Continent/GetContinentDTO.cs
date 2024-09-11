using Ecommerce_Project.Models;

namespace Ecommerce_Project.DTOs.Continent
{
    public class GetContinentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Foreign Keys
        public List<Models.Country>? Countries { get; set; }
    }
}
