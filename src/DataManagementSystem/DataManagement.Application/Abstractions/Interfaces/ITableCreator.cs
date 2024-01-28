namespace DataManagement.Application.Abstractions.Interfaces
{
    public interface ITableCreator
    {
        Task CreateTablesIfNotExist();
    }
}