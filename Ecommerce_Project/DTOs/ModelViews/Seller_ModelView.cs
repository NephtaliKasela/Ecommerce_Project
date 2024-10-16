using Ecommerce_Project.DTOs.Order;
using Ecommerce_Project.DTOs.Product;
using Ecommerce_Project.DTOs.Store;

namespace Ecommerce_Project.DTOs.ModelViews
{
    public class Seller_ModelView
    {
        public GetStoreDTO Store { get; set; }
        public List<GetOrderDTO> Orders { get; set; }
        public List<GetProductDTO> Products { get; set; }

        public Seller_ModelView()
        {
            Store = new GetStoreDTO();
            Orders = new List<GetOrderDTO>();
            Products = new List<GetProductDTO>();
        }
    }
}
