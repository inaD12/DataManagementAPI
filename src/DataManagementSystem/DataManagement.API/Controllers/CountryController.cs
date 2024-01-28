using DataManagement.API.Extensions;
using DataManagement.Application.Abstractions.Interfaces.Services;
using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs.Request;
using DataManagement.Domain.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
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
			ResponseDTO response = await _countryService.GetCountryByNameAsync(CountryName);

			if (!response.Result.IsSuccess)
			{
				return this.ParseAndReturnMessage(response.Result);
			}

			return Ok(response.obj);
		}

		[Authorize]
		[HttpPost("CreateCountry")]
		public async Task<IActionResult> CreateCountry(CreateCountryRequestDTO dto)
		{
			Result result = await _countryService.CreateCountryAsync(dto);

			if (!result.IsSuccess)
			{
				return this.ParseAndReturnMessage(result);
			}

			return Created($"/api/Country/{dto.Name}", dto);
		}

		[Authorize]
		[HttpPut("UpdateCountry/{countryname}")]
		public async Task<IActionResult> UpdateCountry([FromBody] UpdateCountryRequestDTO dto, [FromRoute] string countryname)
		{
			Result result = await _countryService.UpdateCountryAsync(dto, countryname);

			if (!result.IsSuccess)
			{
				return this.ParseAndReturnMessage(result);
			}

			return Ok(result.Error);
		}

		[Authorize(Policy = "RequireAdminRole")]
		[HttpDelete("DeleteCountry/{countryName}")]
		public async Task<IActionResult> DeleteCountry(string countryName)
		{
			Result result = await _countryService.DeleteCountryAsync(countryName);

			if (!result.IsSuccess)
			{
				return this.ParseAndReturnMessage(result);
			}

			return Ok();
		}
	}
}
