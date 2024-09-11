using AutoMapper;
using Ecommerce_Project.Data;
using Ecommerce_Project.DTOs.Category;
using Ecommerce_Project.DTOs.Continent;
using Ecommerce_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Project.Services.ContinentServices
{
    public class ContinentServices : IContinentServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ContinentServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetContinentDTO>>> GetAllContinents()
        {
            var continents = await _context.Continents
                .Include(c => c.Countries)
                .ToListAsync();
            var serviceResponse = new ServiceResponse<List<GetContinentDTO>>()
            {
                Data = continents.Select(p => _mapper.Map<GetContinentDTO>(p)).ToList()
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetContinentDTO>> GetContinentById(int id)
        {
            var continent = await _context.Continents
                .Include(c => c.Countries)
                .FirstOrDefaultAsync(c => c.Id == id);

            var serviceResponse = new ServiceResponse<GetContinentDTO>()
            {
                Data = _mapper.Map<GetContinentDTO>(continent)
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetContinentDTO>>> AddContinent(AddContinentDTO newContinent)
        {
            var serviceResponse = new ServiceResponse<List<GetContinentDTO>>();
            var continent = _mapper.Map<Continent>(newContinent);

            _context.Continents.Add(continent);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Continents
                .Select(c => _mapper.Map<GetContinentDTO>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetContinentDTO>> UpdateContinent(UpdateContinentDTO updatedContinent)
        {
            var serviceResponse = new ServiceResponse<GetContinentDTO>();

            try
            {
                var continent = await _context.Continents
                    .FirstOrDefaultAsync(c => c.Id == updatedContinent.Id);
                if (continent is null) { throw new Exception($"Continent with Id '{updatedContinent.Id}' not found"); }

                continent.Name = updatedContinent.Name;
                continent.Description = updatedContinent.Description;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetContinentDTO>(continent);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetContinentDTO>>> DeleteContinent(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetContinentDTO>>();

            try
            {
                var continent = await _context.Continents.FirstOrDefaultAsync(c => c.Id == id);
                if (continent is null) { throw new Exception($"Continent with Id '{id}' not found"); }

                _context.Continents.Remove(continent);

                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Continents
                    .Select(c => _mapper.Map<GetContinentDTO>(c)).ToListAsync();
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
