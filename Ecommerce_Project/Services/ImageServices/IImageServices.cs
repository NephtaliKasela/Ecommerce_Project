using Ecommerce_Project.Models;
using System.Web;

//using static System.Net.Mime.MediaTypeNames;


namespace Ecommerce_Project.Services.ImageServices
{
    public interface IImageServices
    {
        Task AddProductImage(int productId, IFormFile file);
    }
}
