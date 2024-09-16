using Ecommerce_Project.DTOs.Images.ProductImage;
using Ecommerce_Project.DTOs.Product;
using Ecommerce_Project.Models;

namespace Ecommerce_Project.Services.ImageServices.ProductImageServices
{
    public interface IProductImageServices
    {
        Task<ServiceResponse<GetProductDTO>> AddProductImage(AddProductImageDTO newProductImage);
    }
}
