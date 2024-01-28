namespace DataManagement.Domain.InfrastructureInterfaces
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
        IFullDataRepository CreateFullDataRepository();

	}
}