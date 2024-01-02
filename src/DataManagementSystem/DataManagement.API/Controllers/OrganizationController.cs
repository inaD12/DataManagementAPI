using DataManagement.Application.Services;
using DataManagement.Domain.DTOs.Request;
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
			return await _organizationService.GetOrganizationByNameAsync(OrganizationName);
		}

		[HttpPost("CreateOrganization")]
		public async Task<IActionResult> CreateOrganization(CreateOrganizationRequestDTO dto)
		{
			return await _organizationService.CreateOrganizationAsync(dto);
		}

		[HttpPut("UpdateOrganization")]
		public async Task<IActionResult> UpdateOrganization(UpdateOrganizationRequestDTO dto)
		{
			return await _organizationService.UpdateOrganizationAsync(dto);
		}

		[HttpDelete("DeleteOrganization/{OrganizationName}")]
		public async Task<IActionResult> DeleteOrganization(string OrganizationName)
		{
			return await _organizationService.DeleteOrganizationAsync(OrganizationName);
		}
	}
}
