namespace DataManagement.Infrastructure.Repositories
{
	internal interface IRepositoryHelper
	{
		string GetColumnsForTable(string _tableName);
		string GetPropertyNamesForTable(string _tableName);
		List<string> GetTableProperties(string _tableName);
	}
}