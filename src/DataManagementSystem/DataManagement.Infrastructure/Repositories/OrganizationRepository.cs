using Dapper;
using DataManagement.Application.Abstractions;
using DataManagement.Domain.Entities;
using Serilog;
using static Dapper.SqlMapper;

namespace DataManagement.Infrastructure.Repositories
{
	internal class OrganizationRepository : Repository<Organization>, IOrganizationRepository
	{
		private readonly ISqlConnectionFactory _connectionFactory;
		public OrganizationRepository(ISqlConnectionFactory connectionFactory, IRepositoryHelper repositoryHelper)
			:base(connectionFactory, repositoryHelper)
		{
			_connectionFactory = connectionFactory;
		}

		public override async Task<bool> SoftDeleteByNameAsync(string Name)
		{
			var organization = await GetByNameAsync(Name);

			if (organization == null)
			{
				return false;
			}

			var organizationId = organization.Id;

			bool updateRes = await DeleteIndustryOrganizationRelationship(organizationId);

			if (!updateRes)
			{
				return false;
			}

			return await base.SoftDeleteByNameAsync(Name);
		}

		public async Task<IEnumerable<Industry>> GetIndustriesByOrganizationIdAsync(string organizationId)
		{
			try
			{
				string query = @"
                SELECT I.Name
                FROM [IndustryOrganization] IO
                INNER JOIN [Industry] I ON IO.IndustryId = I.Id
                WHERE IO.OrganizationId = @OrganizationId";

				await using (var connection = _connectionFactory.CreateConnection())
				{
					var result = await connection.QueryAsync<Industry>(query, new { OrganizationId = organizationId });

					return result;
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error in OrganizationRepository, GetIndustriesByOrganizationIdAsync: {ex.Message}");
				return Enumerable.Empty<Industry>();
			}
		}

		private async Task<bool> DeleteIndustryOrganizationRelationship(string organizationId)
		{
			try
			{
				await using (var connection = _connectionFactory.CreateConnection())
				{
					string query = $"DELETE FROM [IndustryOrganization] WHERE OrganizationId = @OrganizationId";
					await connection.ExecuteAsync(query, new { OrganizationId = organizationId });
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error deleting industry-organization relationships in OrganizationRepository: {ex.Message}");
				return false;
			}

			return true;
		}
	}
}
