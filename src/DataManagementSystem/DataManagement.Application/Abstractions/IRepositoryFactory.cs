using DataManagement.Domain.Abstractions;
using DataManagement.Infrastructure.Repositories;

namespace DataManagement.Application.Abstractions
{
	public interface IRepositoryFactory
	{
		ICountryRepository CreateCountryRepository();
		IOrganizationRepository CreateOrganizationRepository();
		IIndustryRepository CreateIndustryRepository();
		IIndustryOrganizationRepository CreateIndustryOrganizationRepository();
		IUserRepository CreateUserRepository();
		IStatsRepository CreateStatsRepository();
		IUserRoleRepository CreateUserRoleRepository();
	}
}