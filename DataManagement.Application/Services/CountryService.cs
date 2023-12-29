using DataManagement.Domain.DTOs;
using DataManagement.Domain.DTOs.Request;
using DataManagement.Domain.DTOs.Response;
using DataManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DataManagement.Application.Services
{
	public class CountryService : ICountryService
	{
		private readonly ICountryRepository repository;

		public CountryService(ICountryRepository repository)
		{
			this.repository = repository;
		}

		public async Task<IActionResult> GetCountryByNameAsync(string countryName)
		{
			Country res = await repository.GetCountryByNameAsync(countryName);

			GetCountryResponseDTO dto = new GetCountryResponseDTO(CountryName: res.CountryName, CreatedAt: res.CreatedAt);

			return new OkObjectResult(dto);
		}

		public async Task<IActionResult> CreateCountryAsync(CreateCountryRequestDTo dto)
		{
			Country country = new Country()
			{
				CountryName = dto.CountryName
			};

			await repository.CreateCountryAsync(country);

			return new OkResult();
		}
	}
}
