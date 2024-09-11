using AutoMapper;
using Ecommerce_Project.Data;
using Ecommerce_Project.DTOs.Category;
using Ecommerce_Project.DTOs.Subcategory;
using Ecommerce_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Project.Services.CategoryServices
{
    public class CategoryServices: ICategoryServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCategoryDTO>>> GetCategories()
        {
            var categories = await _context.Categories
                .Include(c => c.SubCategories)
                .ToListAsync();
            var serviceResponse = new ServiceResponse<List<GetCategoryDTO>>()
            {
                Data = categories.Select(p => _mapper.Map<GetCategoryDTO>(p)).ToList()
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCategoryDTO>> GetCategoryById(int id)
        {
            var category = await _context.Categories
                .Include(c => c.SubCategories)
                .FirstOrDefaultAsync(c => c.Id == id);

            var serviceResponse = new ServiceResponse<GetCategoryDTO>()
            {
                Data = _mapper.Map<GetCategoryDTO>(category)
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCategoryDTO>>> AddCategory(AddCategoryDTO newCategory)
        {
            var serviceResponse = new ServiceResponse<List<GetCategoryDTO>>();
            var category = _mapper.Map<Category>(newCategory);

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.SubCategories
                .Select(c => _mapper.Map<GetCategoryDTO>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCategoryDTO>> UpdateCategory(UpdateCategoryDTO updatedCategory)
        {
            var serviceResponse = new ServiceResponse<GetCategoryDTO>();

            try
            {
                var category = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Id == updatedCategory.Id);
                if (category is null) { throw new Exception($"SubCategory with Id '{updatedCategory.Id}' not found"); }

                category.Name = updatedCategory.Name;
                category.Description = updatedCategory.Description;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCategoryDTO>(category);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCategoryDTO>>> DeleteCategory(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCategoryDTO>>();

            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category is null) { throw new Exception($"Product with Id '{id}' not found"); }

                _context.Categories.Remove(category);

                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Categories
                    .Select(c => _mapper.Map<GetCategoryDTO>(c)).ToListAsync();
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
