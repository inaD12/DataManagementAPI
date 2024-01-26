using DataManagement.Application.Abstractions.Interfaces;

namespace DataManagement.Infrastructure.Repositories
{
    internal class RepositoryFactory : IRepositoryFactory
	{
		private readonly ISqlConnectionFactory _connectionFactory;
		private readonly IRepositoryHelper _repositoryHelper;

		public RepositoryFactory(ISqlConnectionFactory connectionFactory, IRepositoryHelper repositoryHelper)
		{
			_connectionFactory = connectionFactory;
			_repositoryHelper = repositoryHelper;
		}

		public ICountryRepository CreateCountryRepository()
		{
			return new CountryRepository(_connectionFactory, _repositoryHelper);
		}

		public IOrganizationRepository CreateOrganizationRepository()
		{
			return new OrganizationRepository(_connectionFactory, _repositoryHelper);
		}

		public IIndustryRepository CreateIndustryRepository()
		{
			return new IndustryRepository(_connectionFactory, _repositoryHelper);
		}

		public IIndustryOrganizationRepository CreateIndustryOrganizationRepository()
		{
			return new IndustryOrganizationRepository(_connectionFactory, _repositoryHelper);
		}

		public IUserRepository CreateUserRepository()
		{
			return new UserRepository(_connectionFactory, _repositoryHelper);
		}

		public IStatsRepository CreateStatsRepository()
		{
			return new StatsRepository(_connectionFactory);
		}

		public IUserRoleRepository CreateUserRoleRepository()
		{
			return new UserRoleRepository(_connectionFactory, _repositoryHelper);
		}

		public IFullDataRepository CreateFullDataRepository()
		{
			return new FullDataRepository(_connectionFactory);
		}
	}
}
