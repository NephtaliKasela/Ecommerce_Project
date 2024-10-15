using Ecommerce_Project.DTOs.Cart;
using Ecommerce_Project.DTOs.Product;
using Ecommerce_Project.Models;

namespace Ecommerce_Project.Services.CartServices
{
    public interface ICartServices
    {
        Task<ServiceResponse<List<GetCartDTO>>> GetAllCarts();
        Task<ServiceResponse<List<GetCartDTO>>> GetCartsByUserId(ApplicationUser user);
        Task<ServiceResponse<GetCartDTO>> GetCartById(int id);
        Task<ServiceResponse<List<GetCartDTO>>> AddCart(AddCartDTO newCart);
		Task<ServiceResponse<GetCartDTO>> UpdateCart(UpdateCartDTO updatedCart);
		Task<ServiceResponse<GetCartDTO>> CancelCartById(int id);
	}
}
