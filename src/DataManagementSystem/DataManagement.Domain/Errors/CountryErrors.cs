﻿using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.Enums;

namespace DataManagement.Domain.Errors
{
    public static class CountryErrors
	{
		public static readonly Error NotFound = new(ErrorCodes.NotFound, "No country with this name was found");
		public static readonly Error CreationFailure = new(ErrorCodes.CreationFailure, "The creation of the country wasn't successful");
		public static readonly Error NotUpdated = new(ErrorCodes.NotUpdated, "No values were updated");
		public static readonly Error DeleteUnsuccessful = new(ErrorCodes.DeleteUnsuccessful, "No country with this name was found");
	}
}
