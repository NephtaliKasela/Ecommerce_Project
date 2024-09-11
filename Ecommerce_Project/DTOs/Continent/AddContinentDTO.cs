using Ecommerce_Project.Models;

namespace Ecommerce_Project.DTOs.Continent
{
    public class AddContinentDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // Foreign Keys
        //public List<Country>? Countries { get; set; }
    }
}
