using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Project.Controllers
{
    public class SellerController : Controller
    {
        public IActionResult SellerRegistration()
        {
            return View();
        }
    }
}
