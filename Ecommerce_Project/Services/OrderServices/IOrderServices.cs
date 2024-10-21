using Ecommerce_Project.DTOs.Order;
using Ecommerce_Project.DTOs.Product;
using Ecommerce_Project.DTOs.Store;
using Ecommerce_Project.Models;

namespace Ecommerce_Project.Services.OrderServices
{
    public interface IOrderServices
    {
        Task<ServiceResponse<List<GetOrderDTO>>> GetAllOrders();
        Task<ServiceResponse<GetOrderDTO>> GetOrderById(int id);
        Task<ServiceResponse<List<GetOrderDTO>>> GetOrdersBySellerId(int storeId);
        Task<ServiceResponse<List<GetOrderDTO>>> GetOrdersByUserId(string userId);
        Task<ServiceResponse<GetOrderDTO>> GetOrdersByOrderId(string orderId);
        Task<ServiceResponse<GetOrderDTO>> MakeOrderReadyToShip(int orderId);
        Task<ServiceResponse<List<GetOrderDTO>>> AddOrder(AddOrderDTO newOrder);
    }
}
