using DataManagement.Application.Abstractions;
using DataManagement.Domain.DTOs;
using DataManagement.Domain.DTOs.Request;
using DataManagement.Domain.DTOs.Response;
using DataManagement.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DataManagement.Application.Services
{
	public class CountryService : ICountryService
	{
		private readonly ICountryRepository repository;

		public CountryService(IRepositoryFactory repositoryFactory)
		{
			repository = repositoryFactory.CreateCountryRepository();
		}

		public async Task<IActionResult> GetCountryByNameAsync(string countryName)
		{
			Country? res = await repository.GetByNameAsync(countryName);

			if (res is null)
			{
				return new NotFoundObjectResult(CountryErrors.NotFound);
			}

			GetCountryResponseDTO dto = new GetCountryResponseDTO(res.Id, res.Name, (DateTime)res.CreatedAt);

			return new OkObjectResult(dto);
		}

		public async Task<IActionResult> CreateCountryAsync(CreateCountryRequestDTO dto)
		{
			Country country = new Country()
			{
				Name = dto.Name
			};

			country.Set();

			bool res = await repository.CreateAsync(country);

			if(res)
			{
				return new CreatedResult($"/api/Country/CreateAsync", dto);
			}


			return new BadRequestObjectResult(CountryErrors.CreationFailure);
		}

		public async Task<IActionResult> DeleteCountryAsync(string countryName)
		{
			var res = await repository.SoftDeleteByNameAsync(countryName);

			if (res)
			{
				return new OkResult();
			}

			return new BadRequestObjectResult(CountryErrors.DeleteUnsuccessful);
		}

		public async Task<IActionResult> UpdateCountryAsync(UpdateCountryRequestDTO dto)
		{
			Country country = new Country()
			{
				Id = dto.Id,
				Name = dto.Name
			};

			var res =  await repository.UpdateAsync(country);

			if (res)
			{
				return new OkResult();
			}

			return new BadRequestObjectResult(CountryErrors.NotUpdated);
		}
	}
}
