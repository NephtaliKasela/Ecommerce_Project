using Ecommerce_Project.DTOs.Subcategory;
using Ecommerce_Project.Models;
using System.Runtime.CompilerServices;

namespace Ecommerce_Project.Services.SubCategoryServices
{
    public interface ISubCategoryServices
    {
        Task<ServiceResponse<List<GetSubcategoryDTO>>> GetAllSubCategories();
		Task<ServiceResponse<GetSubcategoryDTO>> GetSubCategoryById(int id);

		Task<ServiceResponse<List<GetSubcategoryDTO>>> AddSubCategory(AddSubcategoryDTO newProduct);
        Task<ServiceResponse<GetSubcategoryDTO>> UpdateSubCategory(UpdateSubcategoryDTO updatedSubCategory);
        Task<ServiceResponse<List<GetSubcategoryDTO>>> DeleteSubCategory(int id);
    }
}
