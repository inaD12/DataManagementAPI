using Dapper;
using DataManagement.Application.Abstractions;
using DataManagement.Domain.Entities;
using Serilog;

namespace DataManagement.Infrastructure.Repositories
{
	internal class IndustryRepository : Repository<Industry> , IIndustryRepository
	{
		private readonly ISqlConnectionFactory _connectionFactory;
		public IndustryRepository(ISqlConnectionFactory connectionFactory, IRepositoryHelper repositoryHelper)
			:base(connectionFactory, repositoryHelper)
		{
			_connectionFactory = connectionFactory;
		}

		public override async Task<bool> SoftDeleteByNameAsync(string Name)
		{
			var industry = await GetByNameAsync(Name);

			if (industry == null)
			{
				return false;
			}

			var industryId = industry.Id;

			bool updateRes = await RemoveIndustryFromIndustryOrganization(industryId);

			if (!updateRes)
			{
				return false;
			}

			return await base.SoftDeleteByNameAsync(Name);
		}

		private async Task<bool> RemoveIndustryFromIndustryOrganization(string industryId)
		{
			try
			{
				await using (var connection = _connectionFactory.CreateConnection())
				{
					string query = $"DELETE FROM [IndustryOrganization] WHERE IndustryId = @IndustryId";
					await connection.ExecuteAsync(query, new { IndustryId = industryId });
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error removing related entries in IndustryOrganization in IndustryRepository: {ex.Message}");
				return false;
			}
			return true;
		}
	}
}
