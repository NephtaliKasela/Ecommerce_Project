using Ecommerce_Project.DTOs.Category;
using Ecommerce_Project.Models;

namespace Ecommerce_Project.Services.OtherServices
{
    public interface IOtherServices
    {
        (bool, int) CheckIfInteger(string number);
		Task<ServiceResponse<Category>> GetCategoryById(string categoryId);

	}
}
