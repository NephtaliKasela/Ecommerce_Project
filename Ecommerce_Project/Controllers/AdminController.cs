using Ecommerce_Project.DTOs.Images.ProductImage;
using Ecommerce_Project.DTOs.Product;
using Ecommerce_Project.Services.ImageServices.ProductImageServices;
using Ecommerce_Project.Services.ProductServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Project.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductImageServices _productImageServices;

        public AdminController(IProductService productService, IProductImageServices productImageServices)
        {
            _productService = productService;
            _productImageServices = productImageServices;
        }
        public async Task<ActionResult> Index()
        {
            var products = await _productService.GetAllProducts();
            
            return View(products.Data);
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

        public async Task<ActionResult> AddProductImage(AddProductImageDTO newProductImage)
        {
            var productImage = await _productImageServices.AddProductImage(newProductImage);
            //var product = await _productService.GetProductById(newProductImage.ProductId);
            //if (product. != null)
            //{

            //}
            return RedirectToAction("Products", "Admin");
        }
    }
}
