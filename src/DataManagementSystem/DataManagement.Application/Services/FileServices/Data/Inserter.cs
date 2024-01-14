using DataManagement.Application.Abstractions;
using DataManagement.Application.Abstractions.Interfaces;
using DataManagement.Domain.DBTableProperties;
using DataManagement.Domain.DTOs;
using DataManagement.Domain.Entities;
using Microsoft.Data.SqlClient;
using Serilog;
using System.Data;
using System.Reflection;

namespace DataManagement.Application.Services.FileServices.Data
{
	internal class Inserter : IInserter
	{
		private readonly ISqlConnectionFactory _connectionFactory;

		public Inserter(ISqlConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory;
		}

		public void InsertData(ListWrapper data)
		{
			using (var connection = _connectionFactory.CreateConnection())
			{
				connection.Open();

				if (data.Organizations != null && data.Organizations.Any())
				{
					BulkInsert<Organization>("Organization", data.Organizations, connection, DBTableProperties.Organization);
				}

				if (data.Industries != null && data.Industries.Any())
				{
					BulkInsert<Industry>("Industry", data.Industries, connection, DBTableProperties.Industry);
				}

				if (data.Countries != null && data.Countries.Any())
				{
					BulkInsert<Country>("Country", data.Countries, connection, DBTableProperties.Country);
				}

				if (data.IndOrgs != null && data.IndOrgs.Any())
				{
					BulkInsert<IndustryOrganization>("IndustryOrganization", data.IndOrgs, connection, DBTableProperties.IndustryOrganization);
				}
			}
		}

		private void BulkInsert<T>(string tableName, List<T> data, SqlConnection connection, List<string> propertyList)
		{
			try
			{
				using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
				{
					bulkCopy.DestinationTableName = tableName;

					DataTable dataTable = ConvertListToDataTable(data, propertyList);
					bulkCopy.WriteToServer(dataTable);
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error in BulkInsert, Inserter class: {ex.Message}");
			}
		}

		private DataTable ConvertListToDataTable<T>(List<T> data, List<string> propertyList)
		{
			DataTable dataTable = new DataTable();

			// Add columns based on the provided property list
			foreach (string propertyName in propertyList)
			{
				PropertyInfo property = typeof(T).GetProperty(propertyName);
				Type propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
				dataTable.Columns.Add(propertyName, propertyType);
			}

			// Populate rows
			foreach (T item in data)
			{
				DataRow row = dataTable.NewRow();
				foreach (string propertyName in propertyList)
				{
					PropertyInfo property = typeof(T).GetProperty(propertyName);
					row[propertyName] = property.GetValue(item) ?? DBNull.Value;
				}
				dataTable.Rows.Add(row);
			}

			return dataTable;
		}
	}
}
