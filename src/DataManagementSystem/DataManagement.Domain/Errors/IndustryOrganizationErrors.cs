using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.Enums;

namespace DataManagement.Domain.Errors
{
	public static class IndustryOrganizationErrors
	{
		public static readonly Error CreationFailure = new(ErrorCodes.CreationFailure, "The creation wasn't successful");
		public static readonly Error DeleteUnsuccessful = new(ErrorCodes.DeleteUnsuccessful, "No IndustryOrganization relationship with these parameters was found");
	}
}
