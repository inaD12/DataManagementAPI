using DataManagement.Domain.Abstractions.Result;

namespace DataManagement.Domain.Errors
{
	public static class IndustryOrganizationErrors
	{
		public static readonly Error CreationFailure = new("IndustryOrganization.CreationFailure", "The creation wasn't successful");
		public static readonly Error DeleteUnsuccessful = new("IndustryOrganization.DeleteUnsuccessful", "No IndustryOrganization relationship with these parameters was found");
	}
}
