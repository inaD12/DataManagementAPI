using Dapper;
using DataManagement.Application.Abstractions;
using DataManagement.Domain.Abstractions;
using DataManagement.Domain.Entities;
using Serilog;
using static Dapper.SqlMapper;

namespace DataManagement.Infrastructure.Repositories
{
	internal class IndustryOrganizationRepository : IIndustryOrganizationRepository
	{
		private readonly ISqlConnectionFactory _connectionFactory;
		private readonly IRepositoryHelper _repositoryHelper;
		private readonly string _tableName = "IndustryOrganization";

		public IndustryOrganizationRepository(ISqlConnectionFactory connectionFactory, IRepositoryHelper repositoryHelper)
		{
			_connectionFactory = connectionFactory;
			_repositoryHelper = repositoryHelper;
		}

		public async Task<bool> CreateAsync(IndustryOrganization entity)
		{
			try
			{
				var columns = _repositoryHelper.GetColumnsForTable(_tableName);
				var propertyNames = _repositoryHelper.GetPropertyNamesForTable(_tableName);

				string query = $"INSERT INTO [{_tableName}] ({columns}) VALUES ({propertyNames})";

				await using (var connection = _connectionFactory.CreateConnection())
				{
					var rowsAffected = await connection.ExecuteAsync(query, entity);
					return rowsAffected > 0;
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error in IndustryOrganizationRepository, CreateAsync: {ex.Message}");
				return false;
			}
		}

		public async Task<bool> DeleteAsync(IndustryOrganization entity)
		{
			try
			{
				string query = $"DELETE FROM [{_tableName}] WHERE OrganizationId = @OrganizationId AND IndustryId = @IndustryId";

				var parameters = new { entity.OrganizationId, entity.IndustryId };

				await using (var connection = _connectionFactory.CreateConnection())
				{
					var rowsAffected = await connection.ExecuteAsync(query, parameters);
					return rowsAffected > 0;
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error in IndustryOrganizationRepository, DeleteAsync: {ex.Message}");
				return false;
			}
		}
	}
}
