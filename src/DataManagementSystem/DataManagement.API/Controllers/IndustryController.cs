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
			ResponseDTO response = await _industryService.GetIndustryByNameAsync(industryName);

			if (!response.Result.IsSuccess)
			{
				return this.ParseAndReturnMessage(response.Result);
			}

			return Ok(response.obj);
		}

		[Authorize]
		[HttpPost("CreateIndustry")]
		public async Task<IActionResult> CreateIndustry(CreateIndustryRequestDTO dto)
		{
			Result result = await _industryService.CreateIndustryAsync(dto);

			if (!result.IsSuccess)
			{
				return this.ParseAndReturnMessage(result);
			}

			return Created($"/api/Industry/{dto.Name}", dto);
		}

		[Authorize]
		[HttpPut("UpdateIndustry/{industryname}")]
		public async Task<IActionResult> UpdateIndustry([FromBody] UpdateIndustryRequestDTO dto, [FromRoute] string industryname)
		{
			Result result = await _industryService.UpdateIndustryAsync(dto, industryname);

			if (!result.IsSuccess)
			{
				return this.ParseAndReturnMessage(result);
			}

			return Ok(result.Error);
		}

		[Authorize(Policy = "RequireAdminRole")]
		[HttpDelete("DeleteIndustry/{industryName}")]
		public async Task<IActionResult> DeleteIndustry(string industryName)
		{
			Result result = await _industryService.DeleteIndustryAsync(industryName);

			if (!result.IsSuccess)
			{
				return this.ParseAndReturnMessage(result);
			}

			return Ok();
		}
	}
}
