using Ecommerce_Project.DTOs.Cart;
using Ecommerce_Project.DTOs.Country;
using Ecommerce_Project.DTOs.PaymentMode;

namespace Ecommerce_Project.DTOs.ModelViews
{
    public class Checkout_ModelView
    {
        public List<GetPaymentModeDTO> PaymentModes { get; set; }
        public GetCartDTO Cart { get; set; }
        public List<GetCountryDTO> Countries { get; set; }

        public Checkout_ModelView()
        {
            Cart = new GetCartDTO();
            PaymentModes = new List<GetPaymentModeDTO>();
            Countries = new List<GetCountryDTO>();
        }
    }
}
