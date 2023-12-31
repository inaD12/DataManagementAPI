using DataManagement.Application.Abstractions;
using DataManagement.Domain.DTOs;

namespace DataManagement.Infrastructure.Repositories
{
	internal class CountryRepository : Repository<Country>, ICountryRepository
	{

		public CountryRepository(ISqlConnectionFactory connectionFactory, IRepositoryHelper repositoryHelper)
			:base(connectionFactory, repositoryHelper)
		{
		}

		public override async Task<bool> SoftDeleteByNameAsync(string Name)
		{
			//await base.UpdateAsync(); TODO

			return await base.SoftDeleteByNameAsync(Name);
		}
	}
}
