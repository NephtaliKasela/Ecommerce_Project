using AutoMapper;
using Ecommerce_Project.Data;
using Ecommerce_Project.DTOs.Cart;
using Ecommerce_Project.DTOs.Product;
using Ecommerce_Project.Models;
using Ecommerce_Project.Services.OtherServices;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Project.Services.CartServices
{
    public class CartServices : ICartServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IOtherServices _otherServices;
        public CartServices(ApplicationDbContext context, IMapper mapper, IOtherServices otherServices)
        {
            _context = context;
            _mapper = mapper;
            _otherServices = otherServices;
        }

        public async Task<ServiceResponse<List<GetCartDTO>>> AddCart(AddCartDTO newCart)
        {
            var serviceResponse = new ServiceResponse<List<GetCartDTO>>();
            var cart = _mapper.Map<Cart>(newCart);

            bool result; int number;
            // Get Product
            (result, number) = _otherServices.CheckIfInteger(newCart.ProductId);
            if (result == true)
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == number);
                if (product is not null)
                {
                    cart.Product = product;

                    if (cart.Product.SoldPrice > 0) { cart.Total = newCart.Quantity * Convert.ToDecimal(product.SoldPrice); }
                    else { cart.Total = newCart.Quantity * Convert.ToDecimal(product.Price); }

                    //Save cart
                    _context.Carts.Add(cart);
                    await _context.SaveChangesAsync();
                }
            }

            serviceResponse.Data = await _context.Carts
                .Select(x => _mapper.Map<GetCartDTO>(x)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCartDTO>>> GetCartsByUserId(ApplicationUser user)
        {
            var carts = await _context.Carts
                .Include(x => x.ApplicationUser)
                .Include(x => x.Product)
                .Include(x => x.Product.ProductImages)
                .Where(x => x.ApplicationUser.Id == user.Id && x.Complete == false)
                .ToListAsync();

            var serviceResponse = new ServiceResponse<List<GetCartDTO>>()
            {
                Data = carts.Select(x => _mapper.Map<GetCartDTO>(x)).ToList()
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCartDTO>> GetCartById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCartDTO>();
            try
            {
                var cart = await _context.Carts
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.Product)
                    .Include(x => x.Product.ProductImages)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (cart is null) { throw new Exception($"Cart with Id '{id}' not found"); }

                serviceResponse.Data = _mapper.Map<GetCartDTO>(cart);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCartDTO>>> GetAllCarts()
        {
            var carts = await _context.Carts
                .Include(x => x.ApplicationUser)
                .Include(x => x.Product)
                .Include(x => x.Product.ProductImages)
                .ToListAsync();

            var serviceResponse = new ServiceResponse<List<GetCartDTO>>()
            {
                Data = carts.Select(x => _mapper.Map<GetCartDTO>(x)).ToList()
            };
            return serviceResponse;
        }
    }
}
