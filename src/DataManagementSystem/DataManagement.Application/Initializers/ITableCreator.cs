namespace DataManagement.Application.Initializers
{
	public interface ITableCreator
	{
		Task CreateTablesIfNotExist();
	}
}