using DataManagement.Domain.Abstractions.Result;

namespace DataManagement.Domain.Errors
{
    public static class OrganizationErrors
	{
		public static readonly Error NotFound = new("Organization.NotFound", "No organization with this name was found");
		public static readonly Error CreationFailure = new("Organization.CreationFailure", "The creation of the organization wasn't successful");
		public static readonly Error NotUpdated = new("Organization.NotUpdated", "No values were updated");
		public static readonly Error DeleteUnsuccessful = new("Organization.DeleteUnsuccessful", "No organization with this name was found");
	}
}
