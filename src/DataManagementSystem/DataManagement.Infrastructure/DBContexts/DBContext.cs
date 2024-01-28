using DataManagement.Domain.InfrastructureInterfaces;

namespace DataManagement.Infrastructure.DBContexts
{
	internal class DBContext : IDBContext
	{
		private readonly IRepositoryFactory _repositoryFactory;

		private ICountryRepository _countryRepository;
		private IIndustryOrganizationRepository _industryOrganizationRepository;
		private IIndustryRepository _industryRepository;
		private IOrganizationRepository _organizationRepository;
		private IStatsRepository statsRepository;
		private IUserRepository userRepository;
		private IUserRoleRepository userRoleRepository;
		private IFullDataRepository fullDataRepository;

		public DBContext(IRepositoryFactory repositoryFactory)
		{
			_repositoryFactory = repositoryFactory;
		}

		public ICountryRepository Country
			=> _countryRepository ??= _repositoryFactory.CreateCountryRepository();
		public IIndustryOrganizationRepository IndustryOrganization
			=> _industryOrganizationRepository ??= _repositoryFactory.CreateIndustryOrganizationRepository();
		public IIndustryRepository Industry
			=> _industryRepository ??= _repositoryFactory.CreateIndustryRepository();
		public IOrganizationRepository Organization
			=> _organizationRepository ??= _repositoryFactory.CreateOrganizationRepository();
		public IStatsRepository Stats
			=> statsRepository ??= _repositoryFactory.CreateStatsRepository();
		public IUserRepository User
			=> userRepository ??= _repositoryFactory.CreateUserRepository();
		public IUserRoleRepository UserRole
			=> userRoleRepository ??= _repositoryFactory.CreateUserRoleRepository();
		public IFullDataRepository FullData
			=> fullDataRepository ??= _repositoryFactory.CreateFullDataRepository();


	}
}
