using Ecommerce_Project.Models;

namespace Ecommerce_Project.DTOs.Country
{
	public class UpdateCountryDTO
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
        public string ContinentId { get; set; } = string.Empty;
    }
}
