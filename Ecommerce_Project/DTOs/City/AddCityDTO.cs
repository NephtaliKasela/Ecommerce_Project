using Ecommerce_Project.Models;

namespace Ecommerce_Project.DTOs.City
{
	public class AddCityDTO
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string CountryId { get; set; }
	}
}
