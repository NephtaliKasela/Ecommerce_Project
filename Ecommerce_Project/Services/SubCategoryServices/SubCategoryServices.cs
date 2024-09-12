using AutoMapper;
using Ecommerce_Project.Data;
using Ecommerce_Project.DTOs.Category;
using Ecommerce_Project.DTOs.Subcategory;
using Ecommerce_Project.Models;
using Ecommerce_Project.Services.CategoryServices;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Project.Services.SubCategoryServices
{
    public class SubCategoryServices: ISubCategoryServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
		private readonly ICategoryServices _categoryServices;

		public SubCategoryServices(ApplicationDbContext context, IMapper mapper, ICategoryServices categoryServices)
        {
            _context = context;
            _mapper = mapper;
			_categoryServices = categoryServices;
		}

        public async Task<ServiceResponse<List<GetSubcategoryDTO>>> GetAllSubCategories()
        {
            var subCategories = await _context.SubCategories
                .Include(x => x.Category)
                .Include(x => x.Products)
                .ToListAsync();
            var serviceResponse = new ServiceResponse<List<GetSubcategoryDTO>>()
            {
                Data = subCategories.Select(p => _mapper.Map<GetSubcategoryDTO>(p)).ToList()
            };
            return serviceResponse;
        }

		public async Task<ServiceResponse<GetSubcategoryDTO>> GetSubCategoryById(int id)
		{
            var subCategory = await _context.SubCategories
				.Include(x => x.Category)
				.Include(x => x.Products)
				.FirstOrDefaultAsync(x => x.Id == id);

			var serviceResponse = new ServiceResponse<GetSubcategoryDTO>()
			{
				Data = _mapper.Map<GetSubcategoryDTO>(subCategory)
			};
			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetSubcategoryDTO>>> AddSubCategory(AddSubcategoryDTO newSubCategory)
        {
			var serviceResponse = new ServiceResponse<List<GetSubcategoryDTO>>();
			var subCategory = _mapper.Map<Subcategory>(newSubCategory);

			var category = GetSubCatCategory(newSubCategory.CategoryId);
			subCategory.Category = category.Data;

			await _context.SubCategories.AddAsync(subCategory);
			await _context.SaveChangesAsync();

			serviceResponse.Data = await _context.SubCategories
				.Select(p => _mapper.Map<GetSubcategoryDTO>(p)).ToListAsync();

			return serviceResponse;
        }

        public async Task<ServiceResponse<GetSubcategoryDTO>> UpdateSubCategory(UpdateSubcategoryDTO updatedSubCategory)
        {
            var serviceResponse = new ServiceResponse<GetSubcategoryDTO>();

            try
            {
                var subCategory = await _context.SubCategories
                    .FirstOrDefaultAsync(p => p.Id == updatedSubCategory.Id);
                if (subCategory is null) { throw new Exception($"SubCategory with Id '{updatedSubCategory.Id}' not found"); }

                subCategory.Name = updatedSubCategory.Name;
                subCategory.Description = updatedSubCategory.Description;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetSubcategoryDTO>(subCategory);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetSubcategoryDTO>>> DeleteSubCategory(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetSubcategoryDTO>>();

            try
            {
                var subCategory = await _context.SubCategories.FirstOrDefaultAsync(sc => sc.Id == id);
                if (subCategory is null) { throw new Exception($"Product with Id '{id}' not found"); }

                _context.SubCategories.Remove(subCategory);

                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.SubCategories
                    .Select(sc => _mapper.Map<GetSubcategoryDTO>(sc)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

		private ServiceResponse<Category> GetSubCatCategory(string categoryId)
		{
			var serviceResponse = new ServiceResponse<Category>();

			try
			{
				int convCategoryId = Convert.ToInt32(categoryId);
                var category = _context.Categories.FirstOrDefault(c => c.Id == convCategoryId);


                if (category is null) { throw new Exception($"Category with Id '{convCategoryId}' not found"); }

				serviceResponse.Data = category;
				return serviceResponse;
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
