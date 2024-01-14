namespace DataManagement.Application.Abstractions.Interfaces
{
    public interface IRepositoryHelper
    {
        string GetColumnsForTable(string _tableName);
        string GetPropertyNamesForTable(string _tableName);
        List<string> GetTableProperties(string _tableName);
    }
}