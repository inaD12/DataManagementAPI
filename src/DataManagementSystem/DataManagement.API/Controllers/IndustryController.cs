using DataManagement.Application.Services;
using DataManagement.Domain.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace DataManagement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IndustryController : Controller
	{
		private readonly IIndustryService _industryService;

		public IndustryController(IIndustryService industryService)
		{
			_industryService = industryService;
		}

		[HttpGet("GetIndustry")]
		public async Task<IActionResult> GetIndustry(string industryName)
		{
			return await _industryService.GetIndustryByNameAsync(industryName);
		}

		[HttpPost("CreateIndustry")]
		public async Task<IActionResult> CreateIndustry(CreateIndustryRequestDTO dto)
		{
			return await _industryService.CreateIndustryAsync(dto);
		}

		[HttpPut("UpdateIndustry")]
		public async Task<IActionResult> UpdateIndustry(UpdateIndustryRequestDTO dto)
		{
			return await _industryService.UpdateIndustryAsync(dto);
		}

		[HttpDelete("DeleteIndustry/{industryName}")]
		public async Task<IActionResult> DeleteIndustry(string industryName)
		{
			return await _industryService.DeleteIndustryAsync(industryName);
		}
	}
}
