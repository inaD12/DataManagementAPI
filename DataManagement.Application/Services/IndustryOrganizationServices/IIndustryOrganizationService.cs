using DataManagement.Domain.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace DataManagement.Application.Services.IndustryOrganizationServices
{
    public interface IIndustryOrganizationService
    {
        Task<IActionResult> CreateAsync(IndustryOrganizationRequestDTO dto);
        Task<IActionResult> DeleteAsync(IndustryOrganizationRequestDTO dto);
    }
}