using DataManagement.Application.Services.IndustryOrganizationServices;
using DataManagement.Domain.DTOs.Request;
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
			return await _service.CreateAsync(dto);
		}

		[HttpDelete("DeleteIndustryOrganizationRelationship")]
		public async Task<IActionResult> DeleteIndustryOrganizationRelationship(IndustryOrganizationRequestDTO dto)
		{
			return await _service.DeleteAsync(dto);
		}
	}
}
