using DataManagement.Application.Abstractions;
using DataManagement.Domain.Entities;

namespace DataManagement.Infrastructure.Repositories
{
	internal class IndustryRepository : Repository<Industry> , IIndustryRepository
	{

		public IndustryRepository(ISqlConnectionFactory connectionFactory, IRepositoryHelper repositoryHelper)
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
