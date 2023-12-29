using DataManagement.Domain.DTOs;
using DataManagement.Domain.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace DataManagement.Application.Services
{
	public interface ICountryService
	{
		Task<IActionResult> GetCountryByNameAsync(string countryName);

		Task<IActionResult> CreateCountryAsync(CreateCountryRequestDTo dto);
	}
}