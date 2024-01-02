namespace DataManagement.Application.Abstractions
{
	public interface IDBContext
	{
		ICountryRepository Country { get; }
		IIndustryRepository Industry { get; }
		IOrganizationRepository Organization { get; }
	}
}