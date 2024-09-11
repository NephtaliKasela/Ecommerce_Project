using Ecommerce_Project.Data;
using Ecommerce_Project.Services.CategoryServices;
using Ecommerce_Project.Services.CityServices;
using Ecommerce_Project.Services.ContinentServices;
using Ecommerce_Project.Services.CountryServices;
using Ecommerce_Project.Services.ImageServices;
using Ecommerce_Project.Services.OtherServices;
using Ecommerce_Project.Services.ProductServices;
using Ecommerce_Project.Services.StoreServices;
using Ecommerce_Project.Services.SubCategoryServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//Add Injections
builder.Services.AddScoped<IContinentServices, ContinentServices>();
builder.Services.AddScoped<ICountryServices, CountryServices>();
builder.Services.AddScoped<ICityServices, CityServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();

builder.Services.AddScoped<ISubCategoryServices, SubCategoryServices>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IImageServices, ImageServices>();
builder.Services.AddScoped<IStoreServices, StoreServices>();

builder.Services.AddScoped<IOtherServices, OtherServices>();


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
