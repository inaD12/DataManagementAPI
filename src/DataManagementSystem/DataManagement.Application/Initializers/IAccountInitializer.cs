namespace DataManagement.Application.Initializers
{
	public interface IAccountInitializer
	{
		Task TryCreate();
	}
}