using Ecommerce_Project.DTOs.Cart;
using Ecommerce_Project.DTOs.Product;
using Ecommerce_Project.Models;

namespace Ecommerce_Project.Services.CartServices
{
    public interface ICartServices
    {
        Task<ServiceResponse<List<GetCartDTO>>> GetAllCarts();
        Task<ServiceResponse<GetCartDTO>> GetCartById(int id);
        //Task<ServiceResponse<List<Product>>> GetProductsByStoreId(int storeId);
        Task<ServiceResponse<List<GetCartDTO>>> AddCart(AddCartDTO newCart);
        //Task<ServiceResponse<GetProductDTO>> UpdateProduct(UpdateProductDTO UpdatedProduct);
        //Task<ServiceResponse<List<GetProductDTO>>> DeleteProduct(int id);
    }
}
