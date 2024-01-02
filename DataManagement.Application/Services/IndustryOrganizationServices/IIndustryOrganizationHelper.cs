using DataManagement.Domain.DTOs;
using DataManagement.Domain.DTOs.Request;

namespace DataManagement.Application.Services.IndustryOrganizationServices
{
	internal interface IIndustryOrganizationHelper
	{
		Task<ConvertIndustryOrgRequestDTO> Convert(IndustryOrganizationRequestDTO dto);
	}
}