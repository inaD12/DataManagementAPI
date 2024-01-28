namespace DataManagement.Application.Abstractions.Interfaces
{
    public interface IDBCreator
    {
        Task CreateDatabaseIfNotExists();
    }
}