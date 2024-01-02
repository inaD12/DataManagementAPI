using DataManagement.Domain.Abstractions;

namespace DataManagement.Application.Abstractions
{
	public interface IRepositoryFactory
	{
		ICountryRepository CreateCountryRepository();
		IOrganizationRepository CreateOrganizationRepository();
		IIndustryRepository CreateIndustryRepository();
		IIndustryOrganizationRepository CreateIndustryOrganizationRepository();
	}
}