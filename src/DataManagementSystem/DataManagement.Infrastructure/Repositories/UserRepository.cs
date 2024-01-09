using Dapper;
using DataManagement.Application.Abstractions;
using DataManagement.Domain.DTOs;
using DataManagement.Domain.Entities;
using Serilog;
using static Dapper.SqlMapper;

namespace DataManagement.Infrastructure.Repositories
{
	internal class UserRepository : Repository<User>, IUserRepository
	{
		private readonly ISqlConnectionFactory _connectionFactory;
		public UserRepository(ISqlConnectionFactory connectionFactory, IRepositoryHelper repositoryHelper)
			: base(connectionFactory, repositoryHelper)
		{
			_connectionFactory = connectionFactory;
		}

		public async Task<UserWithRole?> GetByNameWithRoleAsync(string Name)
		{
			try
			{
				UserRole userRole;

				User? user = await base.GetByNameAsync(Name);

				await using (var connection = _connectionFactory.CreateConnection())
				{
					string query = $"SELECT * FROM [UserRole] WHERE DeletedAt IS NULL AND Id = @Id";
					 userRole = await connection.QuerySingleOrDefaultAsync<UserRole>(query, new { Id = user.UserRoleId });
				}

				return new UserWithRole(user.Id, user.Name, user.PasswordHash, user.Salt, user.FirstName, user.LastName, userRole.Name, user.CreatedAt);
			}
			catch (Exception ex)
			{
				Log.Error($"Error in UserRepository, GetByNameWithRoleAsync: {ex.Message}");
				return default;
			}
		}

	}
}
