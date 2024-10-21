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
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IOrderServices _orderServices;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOrderServices orderServices)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _orderServices = orderServices;
        }

        public async Task<IActionResult> Orders()
        {
            //Get the current user
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var orders = await _orderServices.GetOrdersByUserId(user.Id);
                if (orders.Data != null)
                {
                    return View(orders.Data);
                }
            }
            return RedirectToAction("Index", "Home");
            
        }

        public async Task<IActionResult> OrderDetails(int orderId)
        {
            //Get the current user
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var order = await _orderServices.GetOrderById(orderId);
                if (order.Data != null && order.Data.ApplicationUser.Id == user.Id)
                {
                    return View(order.Data);
                }
            }
            return RedirectToAction("Index", "Home");

        }
    }
}
