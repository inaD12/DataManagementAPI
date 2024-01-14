using Dapper;
using DataManagement.Application.Abstractions.Interfaces;
using DataManagement.Domain.DTOs.Stats;
using Serilog;

namespace DataManagement.Infrastructure.Repositories
{
    internal class StatsRepository : IStatsRepository
	{
		private readonly ISqlConnectionFactory _connectionFactory;

		public StatsRepository(ISqlConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory;
		}

		public async Task<ICollection<OrganizationStatistics>?> GetTopTenOrganizationsWithMostWorkers()
		{
			try
			{
				await using (var connection = _connectionFactory.CreateConnection())
				{
					var query = @"
                    SELECT TOP 10
                        o.Name AS OrganizationName,
                        o.NumberOfEmployees,
                        i.Name AS IndustryName,
                        c.Name AS CountryName
                    FROM [Organization] o
                    LEFT JOIN [Country] c ON o.CountryId = c.Id
                    LEFT JOIN [IndustryOrganization] io ON o.Id = io.OrganizationId
                    LEFT JOIN [Industry] i ON io.IndustryId = i.Id
                    WHERE o.DeletedAt IS NULL
                    ORDER BY o.NumberOfEmployees DESC";

					var result = await connection.QueryAsync<OrganizationStatistics>(query);
					return (ICollection<OrganizationStatistics>)result;
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error in StatisticsRepository, GetTopTenOrganizationsWithMostWorkers: {ex.Message}");
				return null;
			}
		}

		public async Task<ICollection<IndustryCountStatistics>?> GetWorkerCountByIndustries()
		{
			try
			{
				await using (var connection = _connectionFactory.CreateConnection())
				{
					var query = @"
                SELECT 
                    SUM(o.NumberOfEmployees) AS WorkerCount,
                    i.Name AS IndustryName
                FROM [Organization] o
                LEFT JOIN [IndustryOrganization] io ON o.Id = io.OrganizationId
                LEFT JOIN [Industry] i ON io.IndustryId = i.Id
                WHERE o.DeletedAt IS NULL
                GROUP BY i.Name";

					var result = await connection.QueryAsync<IndustryCountStatistics>(query);
					return (ICollection<IndustryCountStatistics>)result;
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error in StatisticsRepository, GetWorkerCountByIndustries: {ex.Message}");
				return null;
			}
		}

		public async Task<string> GetTotalWorkerCount()
		{
			try
			{
				await using (var connection = _connectionFactory.CreateConnection())
				{
					var query = @"
                    SELECT 
                        SUM(o.NumberOfEmployees) AS TotalWorkerCount
                    FROM [Organization] o
                    WHERE o.DeletedAt IS NULL";

					var result = await connection.ExecuteScalarAsync<int>(query);
					return $"The total worker count is {result}";
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error in StatisticsRepository, GetTotalWorkerCount: {ex.Message}");
				return string.Empty;
			}
		}
	}
}
