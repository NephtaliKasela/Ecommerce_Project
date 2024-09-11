using Ecommerce_Project.DTOs.Country;
using Ecommerce_Project.DTOs.Subcategory;
using Ecommerce_Project.Models;

namespace Ecommerce_Project.Services.CountryServices
{
	public interface ICountryServices
	{
		Task<ServiceResponse<List<GetCountryDTO>>> GetAllCountries();
		Task<ServiceResponse<GetCountryDTO>> GetCountryById(int id);
		Task<ServiceResponse<List<GetCountryDTO>>> AddCountry(AddCountryDTO newCountry);
		Task<ServiceResponse<GetCountryDTO>> UpdateCountry(UpdateCountryDTO updatedCountry);
		Task<ServiceResponse<List<GetCountryDTO>>> DeleteCountry(int id);
	}
}
