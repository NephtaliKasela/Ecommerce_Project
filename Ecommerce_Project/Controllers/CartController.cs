using Ecommerce_Project.DTOs.Cart;
using Ecommerce_Project.Models;
using Ecommerce_Project.Services.CartServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Project.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICartServices _cartServices;

        public CartController(UserManager<ApplicationUser> userManager, ICartServices cartServices)
        {
            _userManager = userManager;
            _cartServices = cartServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddCart(AddCartDTO newCart)
        {
            //Get the current user
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                newCart.ApplicationUser = user;
                await _cartServices.AddCart(newCart);
            }

            return RedirectToAction("ProductDetails", "Product", new { id = newCart.ProductId });
        }
    }
}
