using DataManagement.Application.Abstractions.Interfaces;
using DataManagement.Domain.DBTableProperties;
using Microsoft.Data.SqlClient;

namespace DataManagement.Application.Initializers
{
	public class TableCreator : ITableCreator
	{
		private readonly ISqlConnectionFactory _connectionFactory;

		public TableCreator(ISqlConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory;
		}
		public async Task CreateTablesIfNotExist()
		{
			try
			{
				await using (var connection = _connectionFactory.CreateConnection())
				{
					connection.Open();

					foreach (var kvp in TableCreationScripts.AllCreationQueries)
					{
						if (!TableExists(connection, kvp.Key))
						{
							CreateTable(connection, kvp.Value);
							Console.WriteLine($"Table '{kvp.Key}' created.");
						}
					}
					//Thread.Sleep( 1000 );
					connection.Close();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in CreateDatabaseAndTablesIfNotExist: {ex.Message}");
			}
		}

		private bool TableExists(SqlConnection connection, string tableName)
		{
			using (SqlCommand command = new SqlCommand($"SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'", connection))
			{
				return command.ExecuteScalar() != null;
			}
		}

		private void CreateTable(SqlConnection connection, string tableQuery)
		{
			using (SqlCommand command = new SqlCommand(tableQuery, connection))
			{
				command.ExecuteNonQuery();
			}
		}
	}
}
