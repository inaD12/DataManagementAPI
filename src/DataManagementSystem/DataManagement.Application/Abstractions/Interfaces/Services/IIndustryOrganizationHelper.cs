using DataManagement.Domain.DTOs;
using DataManagement.Domain.DTOs.Request;

namespace DataManagement.Application.Abstractions.Interfaces.Services
{
    internal interface IIndustryOrganizationHelper
    {
        Task<ConvertIndustryOrgRequestDTO> Convert(IndustryOrganizationRequestDTO dto);
    }
}