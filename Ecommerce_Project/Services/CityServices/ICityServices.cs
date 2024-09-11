using Ecommerce_Project.DTOs.City;
using Ecommerce_Project.DTOs.Country;
using Ecommerce_Project.Models;

namespace Ecommerce_Project.Services.CityServices
{
	public interface ICityServices
	{
		Task<ServiceResponse<List<GetCityDTO>>> GetAllCities();
		Task<ServiceResponse<GetCityDTO>> GetCityById(int id);
		Task<ServiceResponse<List<GetCityDTO>>> AddCity(AddCityDTO newCity);
		Task<ServiceResponse<GetCityDTO>> UpdateCity(UpdateCityDTO updatedCity);
		Task<ServiceResponse<List<GetCityDTO>>> DeleteCity(int id);
	}
}
