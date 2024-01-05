using DataManagement.Application.Abstractions;
using DataManagement.Domain.Entities;

namespace DataManagement.Infrastructure.Repositories
{
	internal class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(ISqlConnectionFactory connectionFactory, IRepositoryHelper repositoryHelper)
			: base(connectionFactory, repositoryHelper)
		{
		}

	}
}
