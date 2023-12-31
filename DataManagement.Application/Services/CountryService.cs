using DataManagement.Application.Abstractions;
using DataManagement.Domain.DTOs;
using DataManagement.Domain.DTOs.Request;
using DataManagement.Domain.DTOs.Response;
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
			Country res = await repository.GetByNameAsync(countryName);

			if (res is null)
			{
				return new NotFoundResult();
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

			await repository.CreateAsync(country);

			return new OkResult();
		}

		public async Task<IActionResult> DeleteCountryAsync(string countryName)
		{
			await repository.SoftDeleteByNameAsync(countryName);

			return new OkResult();
		}

		public async Task<IActionResult> UpdateCountryAsync(UpdateCountryRequestDTO dto)
		{
			Country country = new Country()
			{
				Id = dto.Id,
				Name = dto.Name
			};

			await repository.UpdateAsync(country);

			return new OkResult();
		}
	}
}
