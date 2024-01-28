using DataManagement.Domain.InfrastructureInterfaces;
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

		public SqlConnection CreateMasterConnection()
		{
			string connectionString = _configuration.GetConnectionString("MasterDB");

			if (string.IsNullOrEmpty(connectionString))
			{
				throw new InvalidOperationException("Master database connection string not found in appsettings.json");
			}

			return new SqlConnection(connectionString);
		}
	}
}
