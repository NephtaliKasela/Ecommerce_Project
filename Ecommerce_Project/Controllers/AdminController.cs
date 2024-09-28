using Ecommerce_Project.DTOs.Images.ProductImage;
using Ecommerce_Project.DTOs.Product;
using Ecommerce_Project.Services.CategoryServices;
using Ecommerce_Project.Services.ImageServices.ProductImageServices;
using Ecommerce_Project.Services.ProductServices;
using Ecommerce_Project.Services.SubCategoryServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Project.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductImageServices _productImageServices;
        private readonly ICategoryServices _categoryServices;
        private readonly ISubcategoryServices _subcategoryServices;

        public AdminController(IProductService productService, IProductImageServices productImageServices, ICategoryServices categoryServices, ISubcategoryServices subcategoryServices)
        {
            _productService = productService;
            _productImageServices = productImageServices;
            _categoryServices = categoryServices;
            _subcategoryServices = subcategoryServices;
        }
        public async Task<ActionResult> Index()
        {
            var products = await _productService.GetAllProducts();
            
            return View(products.Data);
        }

        public async Task<ActionResult> GetCategories()
        {
            var categories = await _categoryServices.GetCategories();
            if (categories.Data.Count > 0)
            {
                return View(categories.Data);
            }
            return RedirectToAction("Index", "Admin");
        }

        public async Task<ActionResult> GetSubcategories()
        {
            var subcategories = await _subcategoryServices.GetAllSubCategories();
            if (subcategories.Data.Count > 0)
            {
                return View(subcategories.Data);
            }
            return RedirectToAction("Index", "Admin");
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
            return RedirectToAction("Products", "Admin");
        }

        //public async Task<ActionResult> AddPaymentMode(AddProductImageDTO newProductImage)
        //{
        //    var productImage = await _productImageServices.AddProductImage(newProductImage);
        //    return RedirectToAction("Products", "Admin");
        //}
    }
}
