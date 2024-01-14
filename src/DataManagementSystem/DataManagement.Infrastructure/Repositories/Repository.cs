using Dapper;
using DataManagement.Application.Abstractions.Interfaces;
using DataManagement.Domain.DTOs;
using DataManagement.Domain.Entities.Base;
using Serilog;
using static Dapper.SqlMapper;

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

		public async Task<Dictionary<string, string>> GetAllNamesAndIdsAsync()
		{
			try
			{
				await using (var connection = _connectionFactory.CreateConnection())
				{
					string query = $"SELECT Id, Name FROM [{_tableName}] WHERE DeletedAt IS NULL";
					var result = await connection.QueryAsync<NameAndID>(query);

					var dictionary = result.ToDictionary(nameAndId => nameAndId.Name, nameAndId => nameAndId.Id);

					return dictionary;
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Repository, GetAllNamesAndIdsAsync: {ex.Message}");
				return new Dictionary<string, string>();
			}
		}

		public async Task<TEntity?> GetByIdAsync(string Id)
		{
			try
			{
				await using (var connection = _connectionFactory.CreateConnection())
				{
					string query = $"SELECT * FROM [{_tableName}] WHERE DeletedAt IS NULL AND Id = @Id";
					return await connection.QuerySingleOrDefaultAsync<TEntity>(query, new { Id });
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Repository, GetByIdAsync: {ex.Message}");
				return default;
			}
		}

		public virtual async Task<bool> SoftDeleteByNameAsync(string Name)
		{
			try
			{
				var currentTime = DateTime.Now;

				string query = $"UPDATE [{_tableName}] SET DeletedAt = @CurrentTime WHERE DeletedAt IS NULL AND Name = @Name";
				var parameters = new { CurrentTime = currentTime, Name };

				await using (var sqlConnection = _connectionFactory.CreateConnection())
				{
					var rowsAffected = await sqlConnection.ExecuteAsync(query, parameters);

					return rowsAffected > 0;
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Repository, SoftDeleteByNameAsync: {ex.Message}");
				return false;
			}
		}

		public async Task<bool> CreateAsync(TEntity entity)
		{
			try
			{
				var columns = _repositoryHelper.GetColumnsForTable(_tableName);
				var propertyNames = _repositoryHelper.GetPropertyNamesForTable(_tableName);

				string query = $"INSERT INTO [{_tableName}] ({columns}) VALUES ({propertyNames})";

				await using (var connection = _connectionFactory.CreateConnection())
				{
					var rowsAffected = await connection.ExecuteAsync(query, entity);
					return rowsAffected > 0;
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Repository, CreateAsync: {ex.Message}");
				return false;
			}
		}

		public async Task<bool> UpdateAsync(TEntity entity)
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
					var rowsAffected = await connection.ExecuteAsync(query, entity);
					return rowsAffected > 0;
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error in Repository, UpdateAsync: {ex.Message}");
				return false;
			}
		}
	}
}
