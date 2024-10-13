using AutoMapper;
using Ecommerce_Project.Data;
using Ecommerce_Project.DTOs.PaymentMode;
using Ecommerce_Project.DTOs.Product;
using Ecommerce_Project.Models;
using Ecommerce_Project.Services.OtherServices;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Project.Services.PaymentModeServices
{
    public class PaymentModeServices : IPaymentModeServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IOtherServices _otherServices;
        public PaymentModeServices(ApplicationDbContext context, IMapper mapper, IOtherServices otherServices)
        {
            _context = context;
            _mapper = mapper;
            _otherServices = otherServices;
        }

        public async Task<ServiceResponse<List<GetPaymentModeDTO>>> AddPaymentMode(AddPaymentModeDTO newPaymentMode)
        {
            var serviceResponse = new ServiceResponse<List<GetPaymentModeDTO>>();
            var paymentMode = _mapper.Map<PaymentMode>(newPaymentMode);

            //Save
            _context.PaymentModes.Add(paymentMode);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.PaymentModes
                .Select(x => _mapper.Map<GetPaymentModeDTO>(x)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPaymentModeDTO>>> GetAllPaymentModes()
        {
            var products = await _context.PaymentModes
                .Include(x => x.Orders)
                .ToListAsync();

            var serviceResponse = new ServiceResponse<List<GetPaymentModeDTO>>()
            {
                Data = products.Select(x => _mapper.Map<GetPaymentModeDTO>(x)).ToList()
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPaymentModeDTO>> UpdatePaymentMode(UpdatePaymentModeDTO updatedPaymentMode)
        {
            var serviceResponse = new ServiceResponse<GetPaymentModeDTO>();

            try
            {
                var paymentMode = await _context.PaymentModes
                    .FirstOrDefaultAsync(x => x.Id == updatedPaymentMode.Id);
                if (paymentMode is null) { throw new Exception($"Payment Mode with Id '{updatedPaymentMode.Id}' not found"); }

                paymentMode.Name = updatedPaymentMode.Name;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetPaymentModeDTO>(paymentMode);
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
