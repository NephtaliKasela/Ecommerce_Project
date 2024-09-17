using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Project.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
