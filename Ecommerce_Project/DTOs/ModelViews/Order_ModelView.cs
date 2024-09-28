using Ecommerce_Project.DTOs.Cart;
using Ecommerce_Project.DTOs.Country;
using Ecommerce_Project.DTOs.PaymentMode;

namespace Ecommerce_Project.DTOs.ModelViews
{
    public class Order_ModelView
    {
        public List<GetPaymentModeDTO> PaymentModes {  get; set; }
        public List<GetCartDTO> Carts { get; set; }
        public List<GetCountryDTO> Countries { get; set; }

        public Order_ModelView()
        {
            Carts = new List<GetCartDTO>();
            PaymentModes = new List<GetPaymentModeDTO>();
            Countries = new List<GetCountryDTO>();
        }
    }
}
