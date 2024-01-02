using DataManagement.Domain.Entities;

namespace DataManagement.Domain.Abstractions
{
	public interface IIndustryOrganizationRepository
	{
		Task<bool> CreateAsync(IndustryOrganization entity);
		Task<bool> DeleteAsync(IndustryOrganization entity);
	}
}