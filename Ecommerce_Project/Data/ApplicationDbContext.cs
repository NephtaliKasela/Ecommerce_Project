using Ecommerce_Project.Models.Images;
using Ecommerce_Project.Models.Prices;
using Ecommerce_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Continent> Continents => Set<Continent>();
        public DbSet<Country> Countries => Set<Country>();
        public DbSet<City> Cities => Set<City>();

        public DbSet<Store> Stores => Set<Store>();
        public DbSet<StoreLocation> StoreLocations => Set<StoreLocation>();

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Subcategory> SubCategories => Set<Subcategory>();

        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductPrice> ProductPrices => Set<ProductPrice>();
        public DbSet<ProductImage> ProductImages => Set<ProductImage>();
        public DbSet<Cart> Carts => Set<Cart>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // // Relationship one-to-one between Store and StoreLocation
            builder.Entity<Store>()
            .HasOne(e => e.Location)
            .WithOne(e => e.Store)
            .HasForeignKey<StoreLocation>();

            // Change Asp.Users table name to Users
            builder.Entity<ApplicationUser>(entity => entity.ToTable(name: "Users"));

            // Relationship one-to-one between Store and ApplicationUser
            builder.Entity<ApplicationUser>()
                .HasOne(a => a.Store)
                .WithOne(a => a.ApplicationUser)
                .HasForeignKey<Store>();
        }
    }
}