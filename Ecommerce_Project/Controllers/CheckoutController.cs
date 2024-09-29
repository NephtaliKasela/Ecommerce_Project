using Ecommerce_Project.DTOs.ModelViews;
using Ecommerce_Project.Models;
using Ecommerce_Project.Services.CartServices;
using Ecommerce_Project.Services.CountryServices;
using Ecommerce_Project.Services.OrderServices;
using Ecommerce_Project.Services.PaymentModeServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Project.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderServices _orderServices;
        private readonly ICartServices _cartServices;
        private readonly IPaymentModeServices _paymentModeServices;
        private readonly ICountryServices _countryServices;

        public CheckoutController(UserManager<ApplicationUser> userManager, IOrderServices orderServices, ICartServices cartServices,
            IPaymentModeServices paymentModeServices, ICountryServices countryServices)
        {
            _userManager = userManager;
            _orderServices = orderServices;
            _cartServices = cartServices;
            _paymentModeServices = paymentModeServices;
            _countryServices = countryServices;
        }
        public async Task<IActionResult> Index()
        {
            //Get the current user
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var cards = await _cartServices.GetCartsByUserId(user);
                var paymentModes = await _paymentModeServices.GetAllPaymentModes();
                var countries = await _countryServices.GetAllCountries();
                if (cards.Data.Count > 0 && paymentModes.Data.Count > 0 && countries.Data.Count > 0)
                {
                    var v = new Order_ModelView();
                    v.Carts = cards.Data.Where(x => x.Complete == false).ToList();
                    v.PaymentModes = paymentModes.Data;
                    v.Countries = countries.Data;

                    return View(v);
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
