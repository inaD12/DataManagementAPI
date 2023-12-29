using DataManagement.Application.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataManagement.Infrastructure.Data
{
	public class SqlConnectionFactory : ISqlConnectionFactory
	{
		private readonly IConfiguration _configuration;

		public SqlConnectionFactory(IConfiguration configuration)
		{
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		public SqlConnection CreateConnection()
		{
			string connectionString = _configuration.GetConnectionString("DataManagementDB");

			if (string.IsNullOrEmpty(connectionString))
			{
				throw new InvalidOperationException("Database connection string not found in appsettings.json");
			}

			return new SqlConnection(connectionString);
		}
	}
}
