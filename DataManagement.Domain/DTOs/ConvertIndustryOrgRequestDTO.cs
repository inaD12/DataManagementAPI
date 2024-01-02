using DataManagement.Domain.Entities;

namespace DataManagement.Domain.DTOs
{
	public record ConvertIndustryOrgRequestDTO(IndustryOrganization? IndustryOrganization, bool industryExists = true, bool orgExists = true);
}
