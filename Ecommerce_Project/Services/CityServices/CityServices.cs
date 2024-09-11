using AutoMapper;
using Ecommerce_Project.Data;
using Ecommerce_Project.DTOs.City;
using Ecommerce_Project.DTOs.Country;
using Ecommerce_Project.Models;
using Ecommerce_Project.Services.OtherServices;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Project.Services.CityServices
{
	public class CityServices : ICityServices
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;
		private readonly IOtherServices _otherServices;

		public CityServices(ApplicationDbContext context, IMapper mapper, IOtherServices otherServices)
		{
			_otherServices = otherServices;
			_mapper = mapper;
			_context = context;
		}

		public async Task<ServiceResponse<List<GetCityDTO>>> AddCity(AddCityDTO newCity)
		{
			var serviceResponse = new ServiceResponse<List<GetCityDTO>>();
			var city = _mapper.Map<City>(newCity);

			bool result; int number;

			// Get Continent
			(result, number) = _otherServices.CheckIfInteger(newCity.CountryId);
			if (result == true)
			{
				var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == number);
				if (country is not null)
				{
					city.Country= country;
				}
			}

			await _context.Cities.AddAsync(city);
			await _context.SaveChangesAsync();

			serviceResponse.Data = await _context.Cities
				.Select(c => _mapper.Map<GetCityDTO>(c)).ToListAsync();

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetCityDTO>>> GetAllCities()
		{
			var cities = await _context.Cities
				.Include(x => x.Country)
				.Include(x => x.BodyProducts)
				.ToListAsync();
			var serviceResponse = new ServiceResponse<List<GetCityDTO>>()
			{
				Data = cities.Select(c => _mapper.Map<GetCityDTO>(c)).ToList()
			};
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetCityDTO>> GetCityById(int id)
		{
			var city = await _context.Cities
				.Include(x => x.Country)
				.Include(x => x.BodyProducts)
				.FirstOrDefaultAsync(x => x.Id == id);

			var serviceResponse = new ServiceResponse<GetCityDTO>()
			{
				Data = _mapper.Map<GetCityDTO>(city)
			};
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetCityDTO>> UpdateCity(UpdateCityDTO updatedCity)
		{
			var serviceResponse = new ServiceResponse<GetCityDTO>();

			try
			{
				var city = await _context.Cities
					.FirstOrDefaultAsync(p => p.Id == updatedCity.Id);
				if (city is null) { throw new Exception($"City with Id '{updatedCity.Id}' not found"); }

				city.Name = updatedCity.Name;
				city.Description = updatedCity.Description;

				bool result; int number;

				// Get Country
				(result, number) = _otherServices.CheckIfInteger(updatedCity.CountryId);
				if (result == true)
				{
					var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == number);
					if (country is not null)
					{
						city.Country = country;
					}
				}

				await _context.SaveChangesAsync();

				serviceResponse.Data = _mapper.Map<GetCityDTO>(city);
			}
			catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}
			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetCityDTO>>> DeleteCity(int id)
		{
			var serviceResponse = new ServiceResponse<List<GetCityDTO>>();

			try
			{
				var city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == id);
				if (city is null) { throw new Exception($"City with Id '{id}' not found"); }

				_context.Cities.Remove(city);

				await _context.SaveChangesAsync();

				serviceResponse.Data = await _context.Cities
					.Select(c => _mapper.Map<GetCityDTO>(c)).ToListAsync();
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