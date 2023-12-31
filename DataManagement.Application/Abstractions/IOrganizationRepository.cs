using DataManagement.Domain.Entities;
using DataManagement.Infrastructure.Repositories;

namespace DataManagement.Application.Abstractions
{
	public interface IOrganizationRepository : IRepository<Organization>
	{
	}
}