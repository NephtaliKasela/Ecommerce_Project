using AutoMapper;
using Ecommerce_Project.Data;
using Ecommerce_Project.DTOs.Category;
using Ecommerce_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Project.Services.OtherServices
{
    public class OtherServices : IOtherServices
    {
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public OtherServices(ApplicationDbContext context, IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}
		
        public (bool, int) CheckIfInteger(string number)
        {
            try
            {
                int convNumber = Convert.ToInt32(number);
                return (true, convNumber);
            }
            catch
            {
            }
            return (false, 0);
        }

		public async Task<ServiceResponse<Category>> GetCategoryById(string categoryId)
		{
			var serviceResponse = new ServiceResponse<Category>();

			try
			{
				bool result; int number;
				(result, number) = CheckIfInteger(categoryId);
				if(result)
				{
					var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == number);

					if (category is null) { throw new Exception($"Category with Id '{number}' not found"); }

					serviceResponse.Data =category;
					return serviceResponse;
				}
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
