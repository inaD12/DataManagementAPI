using DataManagement.Domain.Entities.Base;

namespace DataManagement.Domain.InfrastructureInterfaces
{
	public interface IRepository<TEntity> where TEntity : IBaseEntity
    {
        Task<TEntity?> GetByNameAsync(string Name);
        Task<Dictionary<string, string>> GetAllNamesAndIdsAsync();
		Task<TEntity?> GetByIdAsync(string Id);
        Task<bool> SoftDeleteByNameAsync(string Name);
        Task<bool> CreateAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
    }
}