using AutoMapper;
using Ecommerce_Project.Data;
using Ecommerce_Project.DTOs.Product;
using Ecommerce_Project.DTOs.Subcategory;
using Ecommerce_Project.Models;
using Ecommerce_Project.Services.ImageServices;
using Ecommerce_Project.Services.OtherServices;
using Microsoft.EntityFrameworkCore;
//using NuGet.Protocol.Plugins;

namespace Ecommerce_Project.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IOtherServices _otherServices;

        public ProductService(ApplicationDbContext context, IMapper mapper, IOtherServices otherServices)
        {
            _context = context;
            _mapper = mapper;
            _otherServices = otherServices;
        }

        public async Task<ServiceResponse<List<GetProductDTO>>> GetAllProducts()
        {
            var products = await _context.Products
                .Include(x => x.Subcategory)
                .Include(x => x.Store)
                .Include(x => x.ProductImages)
                .ToListAsync();
            var serviceResponse = new ServiceResponse<List<GetProductDTO>>()
            {
                Data = products.Select(x => _mapper.Map<GetProductDTO>(x)).ToList()
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductDTO>> GetProductById(int id)
        {
            var serviceResponse = new ServiceResponse<GetProductDTO>();
            try
            {
                var product = await _context.Products
                    .Include(p => p.Store)
                    .Include(p => p.Subcategory)
                    .Include(p => p.ProductImages)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (product is null) { throw new Exception($"Product with Id '{id}' not found"); }

                serviceResponse.Data = _mapper.Map<GetProductDTO>(product);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByStoreId(int storeId)
        {
            var serviceResponse = new ServiceResponse<List<Product>>();
            try
            {
                var products = await _context.Products
                    .Include(p => p.Store)
                    .Include(p => p.Subcategory)
                    .Include(p => p.ProductImages)
                    .Where(x => x.Store.Id == storeId).ToListAsync();

                if (products is null) { throw new Exception($"Product with Store Id '{storeId}' not found"); }

                serviceResponse.Data = products;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductDTO>>> AddProduct(AddProductDTO newProduct)
        {
            var serviceResponse = new ServiceResponse<List<GetProductDTO>>();
            var product = _mapper.Map<Product>(newProduct);

            bool result; int number;
            // Get Subcategory
            (result, number) = _otherServices.CheckIfInteger(newProduct.SubcategoryId);
            if (result == true)
            {
                var subcategory = await _context.SubCategories.FirstOrDefaultAsync(x => x.Id == number);
                if (subcategory is not null)
                {
                    product.Subcategory = subcategory;
                }
            }

            // Get Store
            (result, number) = _otherServices.CheckIfInteger(newProduct.StoreId);
            if (result == true)
            {
                var store = await _context.Stores.FirstOrDefaultAsync(x => x.Id == number);
                if (store is not null)
                {
                    product.Store = store;
                }
            }

            //Save product
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Products
                .Select(p => _mapper.Map<GetProductDTO>(p)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductDTO>> UpdateProduct(UpdateProductDTO updatedProduct)
        {
            var serviceResponse = new ServiceResponse<GetProductDTO>();

            try
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(x => x.Id == updatedProduct.Id);
                if (product is null) { throw new Exception($"Product with Id '{updatedProduct.Id}' not found"); }

                product.Name = updatedProduct.Name;
                product.Price = updatedProduct.Price;
                product.ShortDescription = updatedProduct.ShortDescription;
                product.LongDescription = updatedProduct.LongDescription;

                bool result; int number;

                // Get the Subcategory
                (result, number) = _otherServices.CheckIfInteger(updatedProduct.SubcategoryId);
                if (result == true)
                {
                    var subcategory = await _context.SubCategories.FirstOrDefaultAsync(sc => sc.Id == number);
                    if (subcategory is not null)
                    {
                        product.Subcategory = subcategory;
                    }

                }

                // Get Store
                (result, number) = _otherServices.CheckIfInteger(updatedProduct.StoreId);
                if (result == true)
                {
                    var store = await _context.Stores.FirstOrDefaultAsync(s => s.Id == number);
                    if (store is not null)
                    {
                        product.Store = store;
                    }
                }

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetProductDTO>(product);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductDTO>>> DeleteProduct(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetProductDTO>>();

            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (product is null) { throw new Exception($"Product with Id '{id}' not found"); }

                _context.Products.Remove(product);

                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Products
                    .Select(x => _mapper.Map<GetProductDTO>(x)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
