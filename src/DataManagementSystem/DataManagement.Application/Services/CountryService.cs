using DataManagement.Domain.Abstractions;
using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs;
using DataManagement.Domain.DTOs.Request;
using DataManagement.Domain.DTOs.Response;
using DataManagement.Domain.Errors;

namespace DataManagement.Application.Services
{
	public class CountryService : ICountryService
	{
		private readonly IDBContext _dBContext;

		public CountryService(IDBContext dBContext)
		{
			_dBContext = dBContext;
		}

		public async Task<ResponseDTO> GetCountryByNameAsync(string countryName)
		{
			Result result = Result.Success();

			Country? res = await _dBContext.Country.GetByNameAsync(countryName);

			if (res is null)
			{
				result = CountryErrors.NotFound;

				return new ResponseDTO(result);
			}

			GetCountryResponseDTO dto = new GetCountryResponseDTO(res.Name, (DateTime)res.CreatedAt);

			return new ResponseDTO(result, dto);
		}

		public async Task<Result> CreateCountryAsync(CreateCountryRequestDTO dto)
		{
			Country country = new Country()
			{
				Name = dto.Name
			};

			country.Set();

			bool res = await _dBContext.Country.CreateAsync(country);

			if(res)
			{
				return Result.Success();
			}

			return CountryErrors.CreationFailure;
		}

		public async Task<Result> DeleteCountryAsync(string countryName)
		{
			var res = await _dBContext.Country.SoftDeleteByNameAsync(countryName);

			if (res)
			{
				return Result.Success();
			}

			return CountryErrors.DeleteUnsuccessful;
		}

		public async Task<Result> UpdateCountryAsync(UpdateCountryRequestDTO dto, string countryName)
		{
			Country? country = await _dBContext.Country.GetByNameAsync(countryName);

			if (country is null)
			{
				return CountryErrors.NotFound;
			}

			country.Name = dto.Name ?? country.Name;

			var res =  await _dBContext.Country.UpdateAsync(country);

			if (res)
			{
				return Result.Success();
			}

			return CountryErrors.NotUpdated;
		}
	}
}
