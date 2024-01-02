using DataManagement.Domain.Abstractions.Result;

namespace DataManagement.Domain.Errors
{
    public static class IndustryErrors
	{
		public static readonly Error NotFound = new("Industry.NotFound", "No industry with this name was found");
		public static readonly Error CreationFailure = new("Industry.CreationFailure", "The creation of the industry wasn't successful");
		public static readonly Error NotUpdated = new("Industry.NotUpdated", "No values were updated");
		public static readonly Error DeleteUnsuccessful = new("Industry.DeleteUnsuccessful", "No industry with this name was found");
	}
}
