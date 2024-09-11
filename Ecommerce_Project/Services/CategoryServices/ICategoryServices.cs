using Ecommerce_Project.DTOs.Category;
using Ecommerce_Project.DTOs.Subcategory;
using Ecommerce_Project.Models;

namespace Ecommerce_Project.Services.CategoryServices
{
    public interface ICategoryServices
    {
        Task<ServiceResponse<List<GetCategoryDTO>>> GetCategories();
        Task<ServiceResponse<GetCategoryDTO>> GetCategoryById(int id);
        Task<ServiceResponse<List<GetCategoryDTO>>> AddCategory(AddCategoryDTO newSubCategory);
        Task<ServiceResponse<GetCategoryDTO>> UpdateCategory(UpdateCategoryDTO updatedCategory);
        Task<ServiceResponse<List<GetCategoryDTO>>> DeleteCategory(int id);
    }
}
