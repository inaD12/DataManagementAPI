using DataManagement.Application.Services;
using DataManagement.Domain.DTOs;
using DataManagement.Domain.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace DataManagement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CountryController : Controller
	{
		private readonly ICountryService _countryService;

		public CountryController(ICountryService countryService)
		{
			_countryService = countryService;
		}

		[HttpGet("GetCountry")]
		public async Task<IActionResult> GetCountry(string CountryName)
		{
			return await _countryService.GetCountryByNameAsync(CountryName);
		}

		[HttpPost("CreateCountry")]
		public async Task<IActionResult> CreateCountry(CreateCountryRequestDTO dto)
		{
			return await _countryService.CreateCountryAsync(dto);
		}

		[HttpPut("UpdateCountry")]
		public async Task<IActionResult> UpdateCountry(UpdateCountryRequestDTO dto)
		{
			return await _countryService.UpdateCountryAsync(dto);
		}

		[HttpDelete("DeleteCountry/{countryName}")]
		public async Task<IActionResult> DeleteCountry(string countryName)
		{
			return await _countryService.DeleteCountryAsync(countryName);
		}
	}
}
