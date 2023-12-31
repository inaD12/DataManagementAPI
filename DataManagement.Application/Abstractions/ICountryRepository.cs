using DataManagement.Domain.DTOs;
using DataManagement.Domain.Entities.Base;

namespace DataManagement.Application.Abstractions
{
	public interface ICountryRepository
	{
		Task SoftDeleteByNameAsync(string Name);
	}
}