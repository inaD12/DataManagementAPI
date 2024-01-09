using DataManagement.Domain.DTOs;
using DataManagement.Domain.Entities;
using DataManagement.Infrastructure.Repositories;

namespace DataManagement.Application.Abstractions
{
	public interface IUserRepository : IRepository<User>
	{
		Task<UserWithRole?> GetByNameWithRoleAsync(string Name);
	}
}