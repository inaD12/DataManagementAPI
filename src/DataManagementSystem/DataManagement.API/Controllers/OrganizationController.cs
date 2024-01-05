using DataManagement.API.Extensions;
using DataManagement.Application.Services;
using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs.Request;
using DataManagement.Domain.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace DataManagement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrganizationController : Controller
	{
		private readonly IOrganizationService _organizationService;

		public OrganizationController(IOrganizationService OrganizationService)
		{
			_organizationService = OrganizationService;
		}

		[HttpGet("GetOrganization")]
		public async Task<IActionResult> GetOrganization(string OrganizationName)
		{
			ResponseDTO response = await _organizationService.GetOrganizationByNameAsync(OrganizationName);

			if (!response.Result.IsSuccess)
			{
				return this.ParseAndReturnMessage(response.Result);
			}

			return Ok(response.obj);
		}

		[HttpPost("CreateOrganization")]
		public async Task<IActionResult> CreateOrganization(CreateOrganizationRequestDTO dto)
		{
			Result result = await _organizationService.CreateOrganizationAsync(dto);

			if (!result.IsSuccess)
			{
				return this.ParseAndReturnMessage(result);
			}

			return Created($"/api/Organization/{dto.Name}", dto);
		}

		[HttpPut("UpdateOrganization/{organizationname}")]
		public async Task<IActionResult> UpdateOrganization([FromBody] UpdateOrganizationRequestDTO dto, [FromRoute] string organizationname)
		{
			Result result = await _organizationService.UpdateOrganizationAsync(dto, organizationname);

			if (!result.IsSuccess)
			{
				return this.ParseAndReturnMessage(result);
			}

			return Ok(result.Error);
		}

		[HttpDelete("DeleteOrganization/{OrganizationName}")]
		public async Task<IActionResult> DeleteOrganization(string OrganizationName)
		{
			Result result = await _organizationService.DeleteOrganizationAsync(OrganizationName);

			if (!result.IsSuccess)
			{
				return this.ParseAndReturnMessage(result);
			}

			return Ok(result.Error);
		}
	}
}
