namespace Ecommerce_Project.Services.DataSeederServices
{
    public interface IDataSeeder
    {
        Task SeedContinentsAsync(int numberOfContinents);
        Task SeedCountries(int numberOfCountries);
        Task SeedCities(int numberOfCities);
        Task SeedStores(int numberOfStores);
        Task SeedCategories(int numberOfCategories);
        Task SeedSubcategories(int numberOfSubcategories);
        Task SeedProducts(int numberOfProducts);
        Task SeedPaymentModes();
    }
}
