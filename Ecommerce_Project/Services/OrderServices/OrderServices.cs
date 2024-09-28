using AutoMapper;
using Ecommerce_Project.Data;
using Ecommerce_Project.DTOs.Cart;
using Ecommerce_Project.DTOs.Order;
using Ecommerce_Project.Models;
using Ecommerce_Project.Services.OtherServices;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Project.Services.OrderServices
{
    public class OrderServices : IOrderServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IOtherServices _otherServices;
        public OrderServices(ApplicationDbContext context, IMapper mapper, IOtherServices otherServices)
        {
            _context = context;
            _mapper = mapper;
            _otherServices = otherServices;
        }
        public async Task<ServiceResponse<List<GetOrderDTO>>> AddOrder(AddOrderDTO newOrder)
        {
            var serviceResponse = new ServiceResponse<List<GetOrderDTO>>();
            var order = _mapper.Map<Order>(newOrder);

            bool result; int number;
            // Get Product
            (result, number) = _otherServices.CheckIfInteger(newOrder.ProductId);
            if (result == true)
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == number);
                if (product is not null)
                {
                    order.Product = product;
                }
            }

            // Get Product
            (result, number) = _otherServices.CheckIfInteger(newOrder.CountryId);
            if (result == true)
            {
                var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == number);
                if (country is not null)
                {
                    order.Country = country;
                }
            }

            // Get Product
            (result, number) = _otherServices.CheckIfInteger(newOrder.PaymentModeId);
            if (result == true)
            {
                var paymentMode = await _context.PaymentModes.FirstOrDefaultAsync(x => x.Id == number);
                if (paymentMode is not null)
                {
                    order.PaymentMode = paymentMode;
                }
            }

            //Save cart
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Orders
                .Select(x => _mapper.Map<GetOrderDTO>(x)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCartDTO>>> GetCartsByUserId(ApplicationUser user)
        {
            var carts = await _context.Carts
                .Include(x => x.ApplicationUser)
                .Include(x => x.Product)
                .Where(x => x.ApplicationUser.Id == user.Id)
                .ToListAsync();

            var serviceResponse = new ServiceResponse<List<GetCartDTO>>()
            {
                Data = carts.Select(x => _mapper.Map<GetCartDTO>(x)).ToList()
            };
            return serviceResponse;
        }

        public Task<ServiceResponse<List<GetOrderDTO>>> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetOrderDTO>> GetOrderById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
