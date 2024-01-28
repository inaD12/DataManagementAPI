namespace DataManagement.Domain.Entities.Base
{
	public abstract class BaseEntity : IBaseEntity
	{
		public void Set()
		{
			Id = Guid.NewGuid().ToString();
			CreatedAt = DateTime.Now;
		}
		public string? Id { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? DeletedAt { get; set; }
	}
}
