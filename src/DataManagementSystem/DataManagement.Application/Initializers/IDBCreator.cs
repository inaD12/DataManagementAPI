
namespace DataManagement.Application.Initializers
{
	public interface IDBCreator
	{
		Task CreateDatabaseIfNotExists();
	}
}