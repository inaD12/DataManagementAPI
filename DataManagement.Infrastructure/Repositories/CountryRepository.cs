using DataManagement.Application.Abstractions;
using DataManagement.Domain.DTOs;
using static Dapper.SqlMapper;

namespace DataManagement.Infrastructure.Repositories
{
	internal class CountryRepository : Repository<Country>, ICountryRepository
	{

		public CountryRepository(ISqlConnectionFactory connectionFactory, IRepositoryHelper repositoryHelper)
			:base(connectionFactory, repositoryHelper)
		{
		}

		public override async Task SoftDeleteByNameAsync(string Name)
		{
			//await base.UpdateAsync(); TODO

			await base.SoftDeleteByNameAsync(Name);
		}
	}
}
