using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.Enums;

namespace DataManagement.Domain.Errors
{
	public static class StatsErrors
	{
		public static readonly Error NotFound = new(ErrorCodes.NotFound, "Error fetching data");
		public static readonly Error MissingData = new(ErrorCodes.NotFound, "No relevant data found");
		public static readonly Error Error = new(ErrorCodes.NotFound, "Error fulfilling your request");
	}
}
