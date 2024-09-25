using Ecommerce_Project.DTOs.Product;
using Ecommerce_Project.DTOs.Subcategory;
using Ecommerce_Project.Models;

namespace Ecommerce_Project.Services.ProductServices
{
    public interface IProductService
    {
        Task<ServiceResponse<List<GetProductDTO>>> GetAllProducts();
        Task<ServiceResponse<GetProductDTO>> GetProductById(int id);
        Task<ServiceResponse<List<Product>>> GetProductsByStoreId(int storeId);
        Task<ServiceResponse<List<GetProductDTO>>> AddProduct(AddProductDTO newProduct);
        Task<ServiceResponse<GetProductDTO>> UpdateProduct(UpdateProductDTO UpdatedProduct);
        Task<ServiceResponse<List<GetProductDTO>>> DeleteProduct(int id);
    }
}