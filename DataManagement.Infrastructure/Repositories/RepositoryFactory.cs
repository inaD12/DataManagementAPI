using DataManagement.Application.Abstractions;

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
	}
}
