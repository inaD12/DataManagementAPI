namespace DataManagement.Domain.DBTableProperties
{
	public static class DBTableProperties
	{
		public static List<string> Country = new List<string> {"Id", "Name", "CreatedAt" };

		public static List<string> Organization = new List<string> { "Id", "OrganizationId", "Name", "Website", "CountryId", "Description", "Founded", "NumberOfEmployees", "CreatedAt"};

		public static List<string> Industry = new List<string> { "Id", "Name", "CreatedAt"};
		public static List<string> IndustryOrganization = new List<string> { "OrganizationId", "IndustryId",};
	}
}
