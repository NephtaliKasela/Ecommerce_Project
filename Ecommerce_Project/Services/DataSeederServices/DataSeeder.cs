using Ecommerce_Project.Data;
using Ecommerce_Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Bogus;
using Ecommerce_Project.Models.Prices;
using Ecommerce_Project.Models.Images;

namespace Ecommerce_Project.Services.DataSeederServices
{
    public class DataSeeder : IDataSeeder
    {
        private readonly ApplicationDbContext _context;

        public DataSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedContinentsAsync(int numberOfContinents)
        {
            var faker = new Faker<Continent>()
                .RuleFor(c => c.Name, f => f.Address.Country())
                .RuleFor(c => c.Description, f => f.Lorem.Sentence());

            var continents = faker.Generate(numberOfContinents);

            await _context.Continents.AddRangeAsync(continents);
            await _context.SaveChangesAsync();
        }

        public async Task SeedCountries(int numberOfCountries)
        {
            for (int i = 1; i <= numberOfCountries; i++)
            {
                Random rnd = new Random();
                int num = rnd.Next(1, numberOfCountries);
                var continent = await _context.Continents.FirstOrDefaultAsync(x => x.Id == num);
                var faker = new Faker<Country>()
                    .RuleFor(c => c.Name, f => f.Address.Country())
                    .RuleFor(c => c.Description, f => f.Lorem.Sentence())
                    .RuleFor(c => c.Continent, continent);

                var countries = faker.Generate(1);

                await _context.Countries.AddRangeAsync(countries);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedCities(int numberOfCities)
        {
            for (int i = 1; i <= numberOfCities; i++)
            {
                Random rnd = new Random();
                int num = rnd.Next(1, numberOfCities);
                var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == num);
                var faker = new Faker<City>()
                    .RuleFor(c => c.Name, f => f.Address.Country())
                    .RuleFor(c => c.Description, f => f.Lorem.Sentence())
                    .RuleFor(c => c.Country, country);

                var cities = faker.Generate(1);

                await _context.Cities.AddRangeAsync(cities);
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task SeedStores(int numberOfStores)
        {
            for (int i = 1; i <= numberOfStores; i++)
            {
                Random rnd = new Random();
                int num = rnd.Next(1, numberOfStores);
                var country = await _context.Countries
                    .Include(x => x.Cities)
                    .FirstOrDefaultAsync(x => x.Id == num);

                var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == num);

                var faker = new Faker<Store>()
                .RuleFor(s => s.Name, f => f.Company.CompanyName())
                .RuleFor(s => s.Description, f => f.Lorem.Sentence())
                .RuleFor(s => s.Name, f => f.Company.CompanyName())
                .RuleFor(s => s.Description, f => f.Lorem.Sentence())
                .RuleFor(s => s.StoreContactInfo, f => f.Phone.PhoneNumber())
                .RuleFor(s => s.StoreLogoUrl, f => f.Internet.Url())
                .RuleFor(s => s.StoreWebsite, f => f.Internet.Url())
                .RuleFor(s => s.StoreEmail, f => f.Internet.Email())
                .RuleFor(s => s.StoreFacebookUrl, f => f.Internet.Url())
                .RuleFor(s => s.StoreTwitterUrl, f => f.Internet.Url())
                .RuleFor(s => s.StoreInstagramUrl, f => f.Internet.Url())
                .RuleFor(s => s.StoreShippingPolicy, f => f.Lorem.Paragraph())
                .RuleFor(s => s.StoreReturnPolicy, f => f.Lorem.Paragraph())
                .RuleFor(s => s.StoreFeatured, f => f.Random.Bool())
                .RuleFor(s => s.StoreActive, f => f.Random.Bool())
                .RuleFor(s => s.StoreCreatedAt, f => f.Date.Past())
                .RuleFor(s => s.StoreUpdatedAt, f => f.Date.Recent())
                .RuleFor(s => s.Country, country) 
                .RuleFor(s => s.City, city);

                var stores = faker.Generate(1);

                await _context.Stores.AddRangeAsync(stores);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedCategories(int numberOfCategories)
        {
            var categoryFaker = new Faker<Category>()
                .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
                .RuleFor(c => c.Description, f => f.Lorem.Sentence());

            var categories = categoryFaker.Generate(numberOfCategories);

            await _context.Categories.AddRangeAsync(categories);
            await _context.SaveChangesAsync();
        }

        public async Task SeedSubcategories(int numberOfSubcategories)
        {
            for (int i = 1; i <= numberOfSubcategories; i++)
            {
                Random rnd = new Random();
                int num = rnd.Next(1, numberOfSubcategories);
                var category = await _context.Categories
                    .FirstOrDefaultAsync(x => x.Id == num);

                var subcategoryFaker = new Faker<Subcategory>()
                .RuleFor(s => s.Name, f => f.Commerce.Categories(1)[0])
                .RuleFor(s => s.Description, f => f.Lorem.Sentence())
                .RuleFor(s => s.Category, category);

                var subcategories = subcategoryFaker.Generate(1);

                _context.SubCategories.AddRange(subcategories);
                _context.SaveChanges();
            }
        }

        public async Task SeedProducts(int numberOfProducts)
        {
            for (int i = 1; i <= numberOfProducts; i++)
            {
                Random rnd = new Random();
                int num = rnd.Next(1, numberOfProducts);
                var store = await _context.Stores
                    .Include(x => x.Country)
                    .Include(x => x.City)
                    .FirstOrDefaultAsync(x => x.Id == num);
                var subcategory = await _context.SubCategories
                    .Include(x => x.Category)
                    .FirstOrDefaultAsync(x => x.Id == num);

                var productFaker = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.ShortDescription, f => f.Commerce.ProductAdjective())
                .RuleFor(p => p.LongDescription, f => f.Lorem.Paragraph())
                .RuleFor(p => p.Price, f => f.Random.Double(90, 100))
                .RuleFor(p => p.SoldPrice, f => f.Random.Double(50, 80))
                .RuleFor(p => p.Brand, f => f.Company.CompanyName())
                .RuleFor(p => p.MadeIn, f => f.Address.Country())
                .RuleFor(p => p.StockQuantity, f => f.Random.Long(0, 1000))
                .RuleFor(p => p.MinimumOrder, f => f.Random.Int(1, 10))
                .RuleFor(p => p.PublicationDate, f => f.Date.Past())
                .RuleFor(p => p.Subcategory, subcategory)
                .RuleFor(p => p.Store, store);

                var products = productFaker.Generate(1);

                await _context.Products.AddRangeAsync(products);
                await _context.SaveChangesAsync();
            }
        }
    }
}
