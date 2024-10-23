using Ecommerce_Project.DTOs.Product;

namespace Ecommerce_Project.DTOs.ModelViews
{
    public class Shop_ModelView
    {
        public List<GetProductDTO> Products { get; set; }

        public Shop_ModelView()
        {
            Products = new List<GetProductDTO>();
        }
    }
}
