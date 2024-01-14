using Dapper;
using DataManagement.Application.Abstractions.Interfaces;
using DataManagement.Domain.DTOs;
using Serilog;

namespace DataManagement.Infrastructure.Repositories
{
    internal class CountryRepository : Repository<Country>, ICountryRepository
	{
		private readonly ISqlConnectionFactory _connectionFactory;
		public CountryRepository(ISqlConnectionFactory connectionFactory, IRepositoryHelper repositoryHelper)
			:base(connectionFactory, repositoryHelper)
		{
			_connectionFactory = connectionFactory;
		}

		public override async Task<bool> SoftDeleteByNameAsync(string Name)
		{
			var country = await GetByNameAsync(Name);

			if (country == null)
			{
				return false;
			}

			var countryId = country.Id;

			bool updateRes = await UpdateOrganizationsCountryIdToNull(countryId);

			if (!updateRes)
			{
				return false;
			}

			return await base.SoftDeleteByNameAsync(Name);
		}

		private async Task<bool> UpdateOrganizationsCountryIdToNull(string countryId)
		{
			try
			{
				await using (var connection = _connectionFactory.CreateConnection())
				{
					string query = $"UPDATE [Organization] SET CountryId = NULL WHERE CountryId = @CountryId";
					await connection.ExecuteAsync(query, new { CountryId = countryId });
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error updating organizations in CountryRepository: {ex.Message}");
				return false;
			}

			return true;
		}
	}
}
