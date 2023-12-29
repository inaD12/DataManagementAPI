namespace DataManagement.Domain.Entities.Base
{
	public interface IBaseEntity
	{
		DateTime CreatedAt { get; }
		DateTime? DeletedAt { get; }
		string Id { get; }
	}
}