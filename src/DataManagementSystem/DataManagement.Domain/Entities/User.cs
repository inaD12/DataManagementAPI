using DataManagement.Domain.Entities.Base;

namespace DataManagement.Domain.Entities
{
	public class User : BaseEntity
	{
		public string Name { get; set; }
		public string PasswordHash { get; set; }
		public string Salt { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
