using DataManagement.Domain.DTOs;
using DataManagement.Domain.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace DataManagement.Application.Services
{
	public interface ICountryService
	{
		Task<IActionResult> GetCountryByNameAsync(string countryName);

		Task<IActionResult> CreateCountryAsync(CreateCountryRequestDTO dto);
		Task<IActionResult> DeleteCountryAsync(string countryName);
		Task<IActionResult> UpdateCountryAsync(UpdateCountryRequestDTO dto);
	}
}