using DataManagement.Application.Abstractions.Interfaces;
using Microsoft.Data.SqlClient;

namespace DataManagement.Application.Initializers
{
	public class DBCreator : IDBCreator
	{
		private readonly ISqlConnectionFactory _connectionFactory;

		public DBCreator(ISqlConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory;
		}

		private const string DatabaseName = "DataManagementDB";

		public async Task CreateDatabaseIfNotExists()
		{
			try
			{
				if (!DatabaseExists(DatabaseName))
				{
					await CreateDatabase(DatabaseName);
					Console.WriteLine($"Database '{DatabaseName}' created.");
					Thread.Sleep(5000);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in CreateDatabaseAndTablesIfNotExist: {ex.Message}");
			}
		}

		private bool DatabaseExists(string databaseName)
		{
			try
			{
				using (var connection = _connectionFactory.CreateConnection())
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand($"SELECT 1 FROM sys.databases WHERE name = '{databaseName}'", connection))
					{
						return command.ExecuteScalar() != null;
					}
				}
			}
			catch
			{
				return false;
			}
		}

		private async Task CreateDatabase(string databaseName)
		{
			try
			{
				await using (var connection = _connectionFactory.CreateMasterConnection())
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand($"CREATE DATABASE [{databaseName}]", connection))
					{
						command.ExecuteNonQuery();
					}

					connection.Close();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error creating database '{databaseName}': {ex.Message}");
			}
		}
	}
}
