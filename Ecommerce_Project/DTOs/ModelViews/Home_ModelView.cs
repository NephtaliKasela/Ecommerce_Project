using Ecommerce_Project.DTOs.Cart;
using Ecommerce_Project.DTOs.Category;
using Ecommerce_Project.DTOs.Product;

namespace Ecommerce_Project.DTOs.ModelViews
{
    public class Home_ModelView
    {
        public List<GetCategoryDTO> Categories { get; set; }
        public List<GetProductDTO> Products { get; set; }
        public List<GetCartDTO> Carts { get; set; }

        public Home_ModelView()
        { 
            Categories = new List<GetCategoryDTO>();
            Products = new List<GetProductDTO>();  
            Carts = new List<GetCartDTO>();
        }
    }
}
