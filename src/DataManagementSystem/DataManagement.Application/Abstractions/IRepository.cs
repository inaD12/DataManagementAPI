using DataManagement.Domain.Entities.Base;

namespace DataManagement.Infrastructure.Repositories
{
	public interface IRepository<TEntity> where TEntity : IBaseEntity
	{
		Task<TEntity?> GetByNameAsync(string Name);
		Task<TEntity?> GetByIdAsync(string Id);
		Task<bool> SoftDeleteByNameAsync(string Name);
		Task<bool> CreateAsync(TEntity entity);
		Task<bool> UpdateAsync(TEntity entity);
	}
}