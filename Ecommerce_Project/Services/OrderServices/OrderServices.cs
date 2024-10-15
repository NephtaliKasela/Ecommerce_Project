using AutoMapper;
using Ecommerce_Project.Data;
using Ecommerce_Project.DTOs.Cart;
using Ecommerce_Project.DTOs.Order;
using Ecommerce_Project.DTOs.Product;
using Ecommerce_Project.DTOs.Store;
using Ecommerce_Project.Models;
using Ecommerce_Project.Services.CartServices;
using Ecommerce_Project.Services.OtherServices;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Project.Services.OrderServices
{
    public class OrderServices : IOrderServices
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartServices _cartServices;
        private readonly IMapper _mapper;
        private readonly IOtherServices _otherServices;
        public OrderServices(ApplicationDbContext context, ICartServices cartServices, IMapper mapper, IOtherServices otherServices)
        {
            _context = context;
            _cartServices = cartServices;
            _mapper = mapper;
            _otherServices = otherServices;
        }

        public async Task<ServiceResponse<List<GetOrderDTO>>> AddOrder(AddOrderDTO newOrder)
        {
            var serviceResponse = new ServiceResponse<List<GetOrderDTO>>();
            var order = _mapper.Map<Order>(newOrder);

            bool result; int number;
            // Get Country
            (result, number) = _otherServices.CheckIfInteger(newOrder.CountryId);
            if (result == true)
            {
                var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == number);
                if (country is not null)
                {
                    order.Country = country;
                }
            }

            // Get Payment Mode
            (result, number) = _otherServices.CheckIfInteger(newOrder.PaymentModeId);
            if (result == true)
            {
                var paymentMode = await _context.PaymentModes.FirstOrDefaultAsync(x => x.Id == number);
                if (paymentMode is not null)
                {
                    order.PaymentMode = paymentMode;
                }
            }

            order.ApplicationUser = newOrder.ApplicationUser;

            // Get all user carts with uncompleted status
            var cart = await _context.Carts
                .Include(x => x.ApplicationUser)
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == newOrder.CartId && x.ApplicationUser == newOrder.ApplicationUser && x.Complete == false);

            if (cart is not null)
            {
                order.Product = cart.Product;
                order.Quantity = cart.Quantity;

                if (cart.Product.SoldPrice > 0)
                {
                    order.Total = Convert.ToDecimal(cart.Product.SoldPrice * cart.Quantity);
                }
                else
                {
                    order.Total = Convert.ToDecimal(cart.Product.Price * cart.Quantity);
                }

                // Generate a unique Order Id
                order.OrderID = GenerateUniqueInt().ToString();

                cart.Complete = true;

                //Add Order to the list of Orders
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
            }

            serviceResponse.Data = await _context.Orders
                .Select(x => _mapper.Map<GetOrderDTO>(x)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetOrderDTO>>> GetAllOrders()
        {
            var orders = await _context.Orders
               .Include(x => x.Product)
               .Include(x => x.Product.ProductImages)
               .Include(x => x.Product.Store)
               .Include(x => x.ApplicationUser)
               .Include(x => x.Country)
               .Include(x => x.PaymentMode)
               .ToListAsync();

            var serviceResponse = new ServiceResponse<List<GetOrderDTO>>()
            {
                Data = orders.Select(x => _mapper.Map<GetOrderDTO>(x)).ToList()
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetOrderDTO>> GetOrderById(int id)
        {
            var serviceResponse = new ServiceResponse<GetOrderDTO>();
            try
            {
                var order = await _context.Orders
                    .Include(x => x.Product)
                    .Include(x => x.Product)
                    .Include(x => x.Product.ProductImages)
                    .Include(x => x.Product.Store)
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.Country)
                    .Include(x => x.PaymentMode)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (order is null) { throw new Exception($"Order with Id '{id}' not found"); }

                serviceResponse.Data = _mapper.Map<GetOrderDTO>(order);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetOrderDTO>>> GetOrdersBySellerId(int storeId)
        {
            var orders = await _context.Orders
               .Where(x => x.Product.Store.Id == storeId)
               .Include(x => x.Product)
               .Include(x => x.Product.ProductImages)
               .Include(x => x.Product.Store)
               .Include(x => x.ApplicationUser)
               .Include(x => x.Country)
               .Include(x => x.PaymentMode)
               .ToListAsync();

            var serviceResponse = new ServiceResponse<List<GetOrderDTO>>()
            {
                Data = orders.Select(x => _mapper.Map<GetOrderDTO>(x)).ToList()
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetOrderDTO>>> GetOrdersByUserId(string userId)
        {
            var orders = await _context.Orders
               .Where(x => x.ApplicationUser.Id == userId)
               .Include(x => x.Product)
               .Include(x => x.Product.ProductImages)
               .Include(x => x.Product.Store)
               .Include(x => x.ApplicationUser)
               .Include(x => x.Country)
               .Include(x => x.PaymentMode)
               .ToListAsync();

            var serviceResponse = new ServiceResponse<List<GetOrderDTO>>()
            {
                Data = orders.Select(x => _mapper.Map<GetOrderDTO>(x)).ToList()
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetOrderDTO>> GetOrdersByOrderId(string orderId)
        {
            var serviceResponse = new ServiceResponse<GetOrderDTO>();
            var order = await _context.Orders
               .Include(x => x.Product)
               .Include(x => x.Product.ProductImages)
               .Include(x => x.Product.Store)
               .Include(x => x.ApplicationUser)
               .Include(x => x.Country)
               .Include(x => x.PaymentMode)
               .FirstOrDefaultAsync(x => x.OrderID == orderId);

            serviceResponse.Data = _mapper.Map<GetOrderDTO>(order);
            return serviceResponse;
        }

        private string GenerateUniqueOrderId()
        {
            return $"{Guid.NewGuid()}-{DateTime.UtcNow.Ticks}";
        }

        private int GenerateUniqueInt()
        {
            Random random = new Random();
            int uniqueInt;

            do
            {
                uniqueInt = random.Next(10000, 99999); // Generates a 5-digit number
            } while (!IsUnique(uniqueInt));

            return uniqueInt;
        }

        private bool IsUnique(int uniqueInt)
        {
            return !_context.Orders.Any(x => x.OrderID == uniqueInt.ToString());
        }

        
    }
}
