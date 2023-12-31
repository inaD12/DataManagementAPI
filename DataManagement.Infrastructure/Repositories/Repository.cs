using Dapper;
using DataManagement.Application.Abstractions;
using DataManagement.Domain.Entities.Base;
using Serilog;

namespace DataManagement.Infrastructure.Repositories
{
	internal abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : IBaseEntity
	{
		private readonly ISqlConnectionFactory _connectionFactory;
		private readonly IRepositoryHelper _repositoryHelper;
		private readonly string _tableName;

		public Repository(ISqlConnectionFactory connectionFactory, IRepositoryHelper repositoryHelper)
		{
			_connectionFactory = connectionFactory;
			_tableName = typeof(TEntity).Name;
			_repositoryHelper = repositoryHelper;
		}

		public async Task<TEntity?> GetByNameAsync(string Name)
		{
			try
			{
				await using (var connection = _connectionFactory.CreateConnection())
				{
					string query = $"SELECT * FROM [{_tableName}] WHERE DeletedAt IS NULL AND Name = @Name";
					return await connection.QuerySingleOrDefaultAsync<TEntity>(query, new { Name });
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Repository, GetByNameAsync: {ex.Message}");
				return default;
			}
		}

		public virtual async Task SoftDeleteByNameAsync(string Name)
		{
			var currentTime = DateTime.Now;

			await using (var sqlConnection = _connectionFactory.CreateConnection())
			{
				await sqlConnection.ExecuteAsync(
					$"UPDATE {_tableName} SET DeletedAt = @CurrentTime WHERE DeletedAt IS NULL AND Name = @Name",
					new { CurrentTime = currentTime, Name }
				);
			}
		}	

		public async Task CreateAsync(TEntity entity)
		{
			try
			{
				var columns = _repositoryHelper.GetColumnsForTable(_tableName);
				var propertyNames = _repositoryHelper.GetPropertyNamesForTable(_tableName);

				string query = $"INSERT INTO [{_tableName}] ({columns}) VALUES ({propertyNames})";

				await using (var connection = _connectionFactory.CreateConnection())
				{

					await connection.ExecuteAsync(query, entity);
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Repository, CreateAsync: {ex.Message}");
			}
		}

		public async Task UpdateAsync(TEntity entity)
		{
			try
			{
				List<string> updateColumns = _repositoryHelper.GetTableProperties(_tableName);

				var nonNullProperties = entity.GetType().GetProperties()
					.Where(prop => prop.GetValue(entity) != null)
					.Select(prop => prop.Name);

				var validColumns = nonNullProperties.Intersect(updateColumns);

				string updateColumnsSql = string.Join(", ", validColumns.Select(column => $"{column} = @{column}"));

				string query = $"UPDATE [{_tableName}] SET {updateColumnsSql} WHERE Id=@Id";

				await using (var connection = _connectionFactory.CreateConnection())
				{
					await connection.ExecuteAsync(query, entity);
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Repository, UpdateAsync: {ex.Message}");
			}
		}
	}
}
