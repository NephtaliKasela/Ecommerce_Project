using Bogus;
using Bogus.DataSets;
using Ecommerce_Project.DTOs.ModelViews;
using Ecommerce_Project.Models;
using Ecommerce_Project.Services.CartServices;
using Ecommerce_Project.Services.CountryServices;
using Ecommerce_Project.Services.OrderServices;
using Ecommerce_Project.Services.PaymentModeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Project.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Index(int cartId)
        {
            //Get the current user
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var cart = await _cartServices.GetCartById(cartId);

                if (cart.Data != null && cart.Data.Complete == false)
                {
                    //var carts = await _cartServices.GetCartsByUserId(user);
                    if (cart.Data.ApplicationUser == user)
                    {
                        var paymentModes = await _paymentModeServices.GetAllPaymentModes();
                        var countries = await _countryServices.GetAllCountries();
                        if (paymentModes.Data.Count > 0 && countries.Data.Count > 0)
                        {
                            var v = new Checkout_ModelView();
                            v.Cart = cart.Data;
                            v.PaymentModes = paymentModes.Data;
                            v.Countries = countries.Data;

                            return View(v);
                        }
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
