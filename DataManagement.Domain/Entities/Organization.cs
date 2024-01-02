using DataManagement.Domain.Entities.Base;

namespace DataManagement.Domain.Entities
{
	public class Organization : BaseEntity
	{
		public string OrganizationId { get; set; }
		public string Name { get; set;}
		public string Website { get; set; }
		public string CountryId { get; set; }
		public string Description { get; set; }
		public int Founded { get; set; }
		public int NumberOfEmployees { get; set; }
	}
}
