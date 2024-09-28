using Ecommerce_Project.DTOs.Order;
using Ecommerce_Project.DTOs.Product;
using Ecommerce_Project.Models;

namespace Ecommerce_Project.Services.OrderServices
{
    public interface IOrderServices
    {
        Task<ServiceResponse<List<GetOrderDTO>>> GetAllOrders();
        Task<ServiceResponse<GetOrderDTO>> GetOrderById(int id);
        Task<ServiceResponse<List<GetOrderDTO>>> AddOrder(AddOrderDTO newOrder);
    }
}
