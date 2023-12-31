using DataManagement.Domain.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace DataManagement.Application.Services
{
	public interface IIndustryService
	{
		Task<IActionResult> CreateIndustryAsync(CreateIndustryRequestDTO dto);
		Task<IActionResult> DeleteIndustryAsync(string industryName);
		Task<IActionResult> GetIndustryByNameAsync(string industryName);
		Task<IActionResult> UpdateIndustryAsync(UpdateIndustryRequestDTO dto);
	}
}