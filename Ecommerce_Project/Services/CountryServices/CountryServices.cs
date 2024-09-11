using AutoMapper;
using Ecommerce_Project.Data;
using Ecommerce_Project.DTOs.Country;
using Ecommerce_Project.DTOs.Subcategory;
using Ecommerce_Project.Models;
using Ecommerce_Project.Services.OtherServices;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Project.Services.CountryServices
{
	public class CountryServices : ICountryServices
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;
        private readonly IOtherServices _otherServices;

		public CountryServices(ApplicationDbContext context, IMapper mapper, IOtherServices otherServices)
		{
            _otherServices = otherServices;
            _mapper = mapper;
			_context = context;
        }

		public async Task<ServiceResponse<List<GetCountryDTO>>> AddCountry(AddCountryDTO newCountry)
		{
			var serviceResponse = new ServiceResponse<List<GetCountryDTO>>();
			var country = _mapper.Map<Country>(newCountry);

			bool result; int number;

			// Get Continent
			(result, number) = _otherServices.CheckIfInteger(newCountry.ContinentId);
			if (result == true)
			{
                var continent = await _context.Continents.FirstOrDefaultAsync(c => c.Id == number);
                if (continent is not null)
				{
					country.Continent = continent;
				}
			}

			await _context.Countries.AddAsync(country);
			await _context.SaveChangesAsync();

			serviceResponse.Data = await _context.Countries
				.Select(p => _mapper.Map<GetCountryDTO>(p)).ToListAsync();

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetCountryDTO>>> GetAllCountries()
		{
			var countries = await _context.Countries
				.Include(x => x.Continent)
				.Include(x => x.Cities)
				.ToListAsync();
			var serviceResponse = new ServiceResponse<List<GetCountryDTO>>()
			{
				Data = countries.Select(p => _mapper.Map<GetCountryDTO>(p)).ToList()
			};
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetCountryDTO>> GetCountryById(int id)
		{
			var country = await _context.Countries
				.Include(x => x.Continent)
				.Include(x => x.Cities)
				.FirstOrDefaultAsync(x => x.Id == id);

			var serviceResponse = new ServiceResponse<GetCountryDTO>()
			{
				Data = _mapper.Map<GetCountryDTO>(country)
			};
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetCountryDTO>> UpdateCountry(UpdateCountryDTO updatedCountry)
		{
			var serviceResponse = new ServiceResponse<GetCountryDTO>();

			try
			{
				var country = await _context.Countries
					.FirstOrDefaultAsync(p => p.Id == updatedCountry.Id);
				if (country is null) { throw new Exception($"Country with Id '{updatedCountry.Id}' not found"); }

				country.Name = updatedCountry.Name;
				country.Description = updatedCountry.Description;

                bool result; int number;

                // Get Continent
                (result, number) = _otherServices.CheckIfInteger(updatedCountry.ContinentId);
                if (result == true)
                {
                    var continent = await _context.Continents.FirstOrDefaultAsync(c => c.Id == number);
                    if (continent is not null)
                    {
                        country.Continent = continent;
                    }
                }

                await _context.SaveChangesAsync();

				serviceResponse.Data = _mapper.Map<GetCountryDTO>(country);
			}
			catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}
			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetCountryDTO>>> DeleteCountry(int id)
		{
			var serviceResponse = new ServiceResponse<List<GetCountryDTO>>();

			try
			{
				var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
				if (country is null) { throw new Exception($"Country with Id '{id}' not found"); }

				_context.Countries.Remove(country);

				await _context.SaveChangesAsync();

				serviceResponse.Data = await _context.Countries
					.Select(c => _mapper.Map<GetCountryDTO>(c)).ToListAsync();
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
