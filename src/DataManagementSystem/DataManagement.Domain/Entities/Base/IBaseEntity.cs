namespace DataManagement.Domain.Entities.Base
{
	public interface IBaseEntity
	{
		void Set();
		DateTime? CreatedAt { get; set; }
		DateTime? DeletedAt { get; set; }
		string Id { get; set; }
	}
}