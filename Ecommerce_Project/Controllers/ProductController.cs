using Ecommerce_Project.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Project.Controllers
{
    public class ProductController : Controller
    {
        public readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product.Data != null) 
            {
                return View(product.Data);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
