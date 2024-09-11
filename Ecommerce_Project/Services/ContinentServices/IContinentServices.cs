using Ecommerce_Project.DTOs.Continent;
using Ecommerce_Project.Models;

namespace Ecommerce_Project.Services.ContinentServices
{
    public interface IContinentServices
    {
        Task<ServiceResponse<List<GetContinentDTO>>> GetAllContinents();
        Task<ServiceResponse<GetContinentDTO>> GetContinentById(int id);
        Task<ServiceResponse<List<GetContinentDTO>>> AddContinent(AddContinentDTO newContinent);
        Task<ServiceResponse<GetContinentDTO>> UpdateContinent(UpdateContinentDTO updatedContinent);
        Task<ServiceResponse<List<GetContinentDTO>>> DeleteContinent(int id);
    }
}
