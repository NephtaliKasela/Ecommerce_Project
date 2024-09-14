using Ecommerce_Project.Services.ProductServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Project.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductService _productService;

        public AdminController(IProductService productService)
        {
            _productService = productService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Products()
        {
            var products = await _productService.GetAllProducts();
            if (products.Data.Count != 0)
            { 
                return View(products.Data); 
            }
            return RedirectToAction("Index", "Admin");
        }

        public async Task<ActionResult> UpdateProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product.Data is not null)
            {
                return View(product.Data);
            }
            return RedirectToAction("Products", "Admin");
        }
    }
}
