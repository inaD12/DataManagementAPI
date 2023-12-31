using DataManagement.Domain.DTOs;
using DataManagement.Infrastructure.Repositories;

namespace DataManagement.Application.Abstractions
{
	public interface ICountryRepository : IRepository<Country>
	{
	}
}