using DataManagement.Application.Abstractions;
using DataManagement.Domain.Entities;

namespace DataManagement.Infrastructure.Repositories
{
	internal class UserRoleRepository : Repository<UserRole> , IUserRoleRepository
	{
		public UserRoleRepository(ISqlConnectionFactory connectionFactory, IRepositoryHelper repositoryHelper)
			: base(connectionFactory, repositoryHelper)
		{
		}
	}
}
