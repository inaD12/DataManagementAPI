using DataManagement.Domain.Entities;
using DataManagement.Domain.InfrastructureInterfaces;

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
