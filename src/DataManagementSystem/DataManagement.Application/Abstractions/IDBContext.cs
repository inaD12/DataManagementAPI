using DataManagement.Application.Abstractions;
using DataManagement.Infrastructure.Repositories;

namespace DataManagement.Domain.Abstractions
{
	public interface IDBContext
	{
		ICountryRepository Country { get; }
		IIndustryRepository Industry { get; }
		IIndustryOrganizationRepository IndustryOrganization { get; }
		IOrganizationRepository Organization { get; }
		IStatsRepository Stats { get; }
		IUserRepository User { get; }
		IUserRoleRepository UserRole { get; }
	}
}