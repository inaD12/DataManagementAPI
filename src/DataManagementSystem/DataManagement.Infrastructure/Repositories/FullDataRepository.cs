using Dapper;
using DataManagement.Domain.DTOs;
using DataManagement.Domain.InfrastructureInterfaces;
using Serilog;

namespace DataManagement.Infrastructure.Repositories
{
	internal class FullDataRepository : IFullDataRepository
	{
		private readonly ISqlConnectionFactory _connectionFactory;

		public FullDataRepository(ISqlConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory;
		}

		public async Task<FileData?> GetOrganizationDataByNameAsync(string organizationName)
		{
			try
			{
				using (var connection = _connectionFactory.CreateConnection())
				{
					connection.Open();

					var query = @"SELECT o.OrganizationId, o.Name, o.Website, c.Name AS Country, o.Description, o.Founded, i.Name AS Industry, o.NumberOfEmployees
                      FROM Organization o
                      LEFT JOIN Country c ON o.CountryId = c.Id
                      LEFT JOIN IndustryOrganization io ON o.Id = io.OrganizationId
                      LEFT JOIN Industry i ON io.IndustryId = i.Id
                      WHERE o.Name = @OrganizationName";

					var organizationData = await connection.QueryFirstOrDefaultAsync<FileData>(query, new { OrganizationName = organizationName });

					return organizationData;
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error in FullDataRepository, GetOrganizationDataByNameAsync: {ex.Message}");
				return null;
			}
		}
	}
}
