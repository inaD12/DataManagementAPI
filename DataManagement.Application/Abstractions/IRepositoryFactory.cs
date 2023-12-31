namespace DataManagement.Application.Abstractions
{
	public interface IRepositoryFactory
	{
		ICountryRepository CreateCountryRepository();
	}
}