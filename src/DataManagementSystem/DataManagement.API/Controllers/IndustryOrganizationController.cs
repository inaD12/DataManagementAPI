using DataManagement.API.Extensions;
using DataManagement.Application.Services.IndustryOrganizationServices;
using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataManagement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IndustryOrganizationController : Controller
	{
		private readonly IIndustryOrganizationService _service;

		public IndustryOrganizationController(IIndustryOrganizationService service)
		{
			_service = service;
		}

		[HttpPost("CreateIndustryOrganizationRelationship")]
		public async Task<IActionResult> CreateIndustryOrganizationRelationship(IndustryOrganizationRequestDTO dto)
		{
			Result result = await _service.CreateAsync(dto);

			if (!result.IsSuccess)
			{
				return this.ParseAndReturnMessage(result);
			}

			return Created($"/api/IndustryOrganization/{dto.IndustryName}/{dto.OrganizationName}", dto);
		}

		[Authorize(Policy = "RequireAdminRole")]
		[HttpDelete("DeleteIndustryOrganizationRelationship")]
		public async Task<IActionResult> DeleteIndustryOrganizationRelationship(IndustryOrganizationRequestDTO dto)
		{
			Result result = await _service.DeleteAsync(dto);

			if (!result.IsSuccess)
			{
				return this.ParseAndReturnMessage(result);
			}

			return Ok(result.Error);
		}
	}
}
