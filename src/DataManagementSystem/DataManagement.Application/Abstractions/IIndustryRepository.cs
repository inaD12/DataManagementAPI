using DataManagement.Domain.DTOs;
using DataManagement.Domain.Entities;
using DataManagement.Infrastructure.Repositories;

namespace DataManagement.Application.Abstractions
{
	public interface IIndustryRepository : IRepository<Industry>
	{
	}
}