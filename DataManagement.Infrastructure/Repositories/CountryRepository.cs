using Dapper;
using DataManagement.Application.Abstractions;
using DataManagement.Domain.DTOs;
using Microsoft.Data.SqlClient;

namespace DataManagement.Infrastructure.Repositories
{
	public class CountryRepository : ICountryRepository
	{
		private readonly ISqlConnectionFactory _connectionFactory;

		public CountryRepository(ISqlConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory;
		}

		public async Task<Country> GetCountryByNameAsync(string countryName)
		{
			await using SqlConnection sqlConnection = _connectionFactory.CreateConnection();

			string query = "SELECT * FROM Country WHERE CountryName = @CountryName AND DeletedAt IS NULL";
			var parameters = new { CountryName = countryName };
			var result = await sqlConnection.QuerySingleOrDefaultAsync<Country>(query, parameters);

			return result;
		}

		public async Task SoftDeleteCountryAsync(string countryName)
		{
			await using SqlConnection sqlConnection = _connectionFactory.CreateConnection();

			string updateQuery = "UPDATE Country SET DeletedAt = @CurrentTime WHERE CountryName = @CountryName";
			var parameters = new { CountryName = countryName, CurrentTime = DateTime.UtcNow };

			await sqlConnection.ExecuteAsync(updateQuery, parameters);
		}

		public async Task CreateCountryAsync(Country country)
		{
			await using SqlConnection sqlConnection = _connectionFactory.CreateConnection();

			var parameters = new
			{
				Id = country.Id,
				CountryName = country.CountryName,
				CreatedAt = country.CreatedAt,
			};

			string insertQuery = @"
            INSERT INTO Country (Id, CountryName, CreatedAt)
            VALUES (@Id, @CountryName, @CreatedAt)";

			await sqlConnection.ExecuteAsync(insertQuery, parameters);
		}

		public async Task UpdateCountryAsync(Country country)
		{
			await using SqlConnection sqlConnection = _connectionFactory.CreateConnection();

			var parameters = new
			{
				Id = country.Id,
				CountryName = country.CountryName,
			};

			string updateQuery = $@"
            UPDATE Country
            SET
                CountryName = @CountryName,
				WHERE Id = @Id";

			await sqlConnection.ExecuteAsync(updateQuery, parameters);
		}
	}
}
