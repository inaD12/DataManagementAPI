namespace DataManagement.Application.Abstractions.Interfaces
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
		IFullDataRepository FullData { get; }

	}
}