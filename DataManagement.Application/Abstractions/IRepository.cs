using DataManagement.Domain.Entities.Base;

namespace DataManagement.Infrastructure.Repositories
{
	public interface IRepository<TEntity> where TEntity : IBaseEntity
	{
		Task<TEntity?> GetByNameAsync(string Name);
		Task SoftDeleteByNameAsync(string Name);
		Task CreateAsync(TEntity entity);
		Task UpdateAsync(TEntity entity);
	}
}