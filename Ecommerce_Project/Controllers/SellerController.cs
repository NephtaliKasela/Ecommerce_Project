using Ecommerce_Project.Models;
using Ecommerce_Project.Services.OrderServices;
using Ecommerce_Project.Services.ProductServices;
using Ecommerce_Project.Services.StoreServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Project.Controllers
{
    [Authorize]
    public class SellerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IStoreServices _storeServices;
        private readonly IProductService _productService;
        private readonly IOrderServices _orderServices;

        public SellerController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IStoreServices storeServices, IProductService productService, IOrderServices orderServices)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _storeServices = storeServices;
            _productService = productService;
            _orderServices = orderServices;
        }

        public async Task<IActionResult> Dashboard()
        {
            //Get the current user
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var store = await _storeServices.GetStoreByUserId(user.Id);
                if (store.Data != null) 
                {
                    return View(store.Data);
                }
            }
            return RedirectToAction("SellerRegistration");
        }
        public async Task<IActionResult> SellerRegistration()
        {
            //Get the current user
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var store = await _storeServices.GetStoreByUserId(user.Id);
                if (store.Data != null)
                {
                    return RedirectToAction("Dashboard");
                }
            }
            return View();
        }

        // Product section

        // Get Seller Products
        public async Task<IActionResult> SellerProducts()
        {
            //Get the current user
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var store = await _storeServices.GetStoreByUserId(user.Id);
                if (store.Data != null)
                {
                    var products = await _productService.GetProductsByStoreId(store.Data.Id);
                    store.Data.Products = products.Data;
                    return View(store.Data);
                }
            }
            return RedirectToAction("SellerRegistration");
        }

        // Order section

        // Get Seller Orders
        public async Task<IActionResult> GetOrders()
        {
            //Get the current user
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var store = await _storeServices.GetStoreByUserId(user.Id);
                if (store.Data != null)
                {
                    var orders = await _orderServices.GetOrdersBySellerId(store.Data.Id);
                    if (orders.Data != null)
                    {
                        return View(orders.Data);
                    }
                }
            }
            return RedirectToAction("SellerRegistration");
        }
    }
}
