using Ecommerce_Project.DTOs.PaymentMode;
using Ecommerce_Project.Models;

namespace Ecommerce_Project.Services.PaymentModeServices
{
    public interface IPaymentModeServices
    {
        Task<ServiceResponse<List<GetPaymentModeDTO>>> AddPaymentMode(AddPaymentModeDTO newPaymentMode);
        Task<ServiceResponse<List<GetPaymentModeDTO>>> GetAllPaymentModes();
        Task<ServiceResponse<GetPaymentModeDTO>> UpdatePaymentMode(UpdatePaymentModeDTO updatedPaymentMode);
    }
}
