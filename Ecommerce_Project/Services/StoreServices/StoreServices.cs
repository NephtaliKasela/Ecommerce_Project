using AutoMapper;
using Ecommerce_Project.Data;
using Ecommerce_Project.DTOs.Category;
using Ecommerce_Project.DTOs.Product;
using Ecommerce_Project.DTOs.Store;
using Ecommerce_Project.Models;
using Ecommerce_Project.Models.Images;
using Ecommerce_Project.Services.ImageServices.ProductImageServices;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Project.Services.StoreServices
{
    public class StoreServices: IStoreServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductImageServices _productImageServices;
        private readonly IMapper _mapper;

        public StoreServices(ApplicationDbContext context, IProductImageServices productImageServices, IMapper mapper)
        {
            _context = context;
            _productImageServices = productImageServices;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetStoreDTO>>> GetAllStores()
        {
            var stores = await _context.Stores
                .ToListAsync();
            var serviceResponse = new ServiceResponse<List<GetStoreDTO>>()
            {
                Data = stores.Select(s => _mapper.Map<GetStoreDTO>(s)).ToList()
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetStoreDTO>> GetStoreById(int id)
        {
            var store = await _context.Stores
                .FirstOrDefaultAsync(s => s.Id == id);

            var serviceResponse = new ServiceResponse<GetStoreDTO>()
            {
                Data = _mapper.Map<GetStoreDTO>(store)
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetStoreDTO>> GetStoreByUserId(string userId)
        {
            var agent = await _context.Stores
                .Include(x => x.ApplicationUser)
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.ApplicationUser.Id == userId);

            var serviceResponse = new ServiceResponse<GetStoreDTO>()
            {
                Data = _mapper.Map<GetStoreDTO>(agent)
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetStoreDTO>>> AddStore(AddStoreDTO newStore)
        {
            var serviceResponse = new ServiceResponse<List<GetStoreDTO>>();
            var store = _mapper.Map<Store>(newStore);

            _context.Stores.Add(store);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Stores
                .Select(s => _mapper.Map<GetStoreDTO>(s)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetStoreDTO>> UpdateStore(UpdateStoreDTO updatedStore)
        {
            var serviceResponse = new ServiceResponse<GetStoreDTO>();

            try
            {
                var store = await _context.Stores
                    .FirstOrDefaultAsync(c => c.Id == updatedStore.Id);
                if (store is null) { throw new Exception($"SubCategory with Id '{updatedStore.Id}' not found"); }

                store.Name = updatedStore.Name;
                store.Description = updatedStore.Description;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetStoreDTO>(store);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetStoreDTO>>> DeleteStore(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetStoreDTO>>();

            try
            {
                var store = await _context.Stores.FirstOrDefaultAsync(s => s.Id == id);
                if (store is null) { throw new Exception($"Product with Id '{id}' not found"); }

                _context.Stores.Remove(store);

                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Stores
                    .Select(c => _mapper.Map<GetStoreDTO>(c)).ToListAsync();
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
