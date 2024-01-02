using DataManagement.Domain.Abstractions.Result;

namespace DataManagement.Domain.Errors
{
    public static class CountryErrors
	{
		public static readonly Error NotFound = new("Country.NotFound", "No country with this name was found");
		public static readonly Error CreationFailure = new("Country.CreationFailure", "The creation of the country wasn't successful");
		public static readonly Error NotUpdated = new("Country.NotUpdated", "No values were updated");
		public static readonly Error DeleteUnsuccessful = new("Country.DeleteUnsuccessful", "No country with this name was found");
	}
}
