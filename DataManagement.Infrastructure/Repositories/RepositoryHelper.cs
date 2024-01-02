using DataManagement.Domain.DBTableProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement.Infrastructure.Repositories
{
	internal class RepositoryHelper : IRepositoryHelper
	{
		public string GetColumnsForTable(string _tableName)
		{
			var tableProperties = GetTableProperties(_tableName);
			return string.Join(", ", tableProperties);
		}

		public string GetPropertyNamesForTable(string _tableName)
		{
			var tableProperties = GetTableProperties(_tableName);
			return string.Join(", ", tableProperties.Select(p => $"@{p}"));
		}

		public List<string> GetTableProperties(string _tableName)
		{
			switch (_tableName)
			{
				case "Country":
					return DBTableProperties.Country;

				case "Organization":
					return DBTableProperties.Organization;

				case "Industry":
					return DBTableProperties.Industry;

				case "IndustryOrganization":
					return DBTableProperties.IndustryOrganization;


				default:
					throw new NotSupportedException($"Table '{_tableName}' not supported.");
			}
		}
	}
}
