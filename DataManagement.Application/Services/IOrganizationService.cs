using DataManagement.Domain.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace DataManagement.Application.Services
{
	public interface IOrganizationService
	{
		Task<IActionResult> CreateOrganizationAsync(CreateOrganizationRequestDTO dto);
		Task<IActionResult> DeleteOrganizationAsync(string OrganizationName);
		Task<IActionResult> GetOrganizationByNameAsync(string OrganizationName);
		Task<IActionResult> UpdateOrganizationAsync(UpdateOrganizationRequestDTO dto);
	}
}