using Ecommerce_Project.DTOs.Cart;
using Ecommerce_Project.DTOs.ModelViews;
using Ecommerce_Project.Models;
using Ecommerce_Project.Services.CartServices;
using Ecommerce_Project.Services.CategoryServices;
using Ecommerce_Project.Services.CityServices;
using Ecommerce_Project.Services.ContinentServices;
using Ecommerce_Project.Services.CountryServices;
using Ecommerce_Project.Services.DataSeederServices;
using Ecommerce_Project.Services.PaymentModeServices;
using Ecommerce_Project.Services.ProductServices;
using Ecommerce_Project.Services.StoreServices;
using Ecommerce_Project.Services.SubCategoryServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ecommerce_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IContinentServices _continentServices;
        private readonly ICountryServices _countryServices;
        private readonly ICityServices _cityServices;
        private readonly IStoreServices _storeServices;
        private readonly ICategoryServices _categoryServices;
        private readonly ISubcategoryServices _subcategoryServices;
        private readonly IProductService _productService;
        private readonly IPaymentModeServices _paymentModeServices;
        private readonly ICartServices _cartServices;
        private readonly IDataSeeder _dataSeeder;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, IContinentServices continentServices, ICountryServices countryServices,
                                ICityServices cityServices, IStoreServices storeServices, ICategoryServices categoryServices,
                                ISubcategoryServices subcategoryServices, IProductService productService, IPaymentModeServices paymentModeServices,
                                ICartServices cartServices, IDataSeeder dataSeeder)
        {
            _logger = logger;
            _userManager = userManager;
            _continentServices = continentServices;
            _countryServices = countryServices;
            _cityServices = cityServices;
            _storeServices = storeServices;
            _categoryServices = categoryServices;
            _subcategoryServices = subcategoryServices;
            _productService = productService;
            _paymentModeServices = paymentModeServices;
            _cartServices = cartServices;
            _dataSeeder = dataSeeder;
        }

        public async Task<IActionResult> Index()
        {
            int n = 10;
            var continents = await _continentServices.GetAllContinents();
            if(continents.Data.Count == 0)
            {
                await _dataSeeder.SeedContinentsAsync(n);
            }

            var countries = await _countryServices.GetAllCountries();
            if (countries.Data.Count == 0)
            {
                await _dataSeeder.SeedCountries(n);
            }

            var cities = await _cityServices.GetAllCities();
            if (cities.Data.Count == 0)
            {
                await _dataSeeder.SeedCities(n);
            }

            var stores = await _storeServices.GetAllStores();
            if (stores.Data.Count == 0)
            {
                await _dataSeeder.SeedStores(n);
            }

            var categories = await _categoryServices.GetCategories();
            if (categories.Data.Count == 0)
            {
                await _dataSeeder.SeedCategories(n);
            }

            var subcategory = await _subcategoryServices.GetAllSubCategories();
            if (subcategory.Data.Count == 0)
            {
                await _dataSeeder.SeedSubcategories(n);
            }

            var paymentModes = await _paymentModeServices.GetAllPaymentModes();
            if (paymentModes.Data.Count == 0)
            {
                await _dataSeeder.SeedPaymentModes();
            }

            var products = await _productService.GetAllProducts();
            if (products.Data.Count == 0)
            {
                await _dataSeeder.SeedProducts(n);
            }

            var v = new Home_ModelView();
            v.Categories = categories.Data;
            v.Products = products.Data;

            //Get the current user
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var carts = await _cartServices.GetCartsByUserId(user);
                v.Carts = carts.Data.Where(x => x.Complete == false).ToList();
            }
            else 
            { 
                var carts = new List<GetCartDTO>(); 
                v.Carts = carts; 
            }

            return View(v);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
