using Ecommerce_Project.DTOs.ModelViews;
using Ecommerce_Project.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Project.Controllers
{
    public class ShopController : Controller
    {
        public readonly IProductService _productService;

        public ShopController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProducts();
            var v = new Shop_ModelView();
            v.Products = products.Data;
            return View(v);
        }
    }
}
