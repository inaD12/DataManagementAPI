using DataManagement.Domain.DTOs;

namespace DataManagement.Infrastructure.Repositories
{
	public interface ICountryRepository
	{
		Task CreateCountryAsync(Country country);
		Task<Country> GetCountryByNameAsync(string countryName);
		Task SoftDeleteCountryAsync(string countryName);
		Task UpdateCountryAsync(Country country);
	}
}