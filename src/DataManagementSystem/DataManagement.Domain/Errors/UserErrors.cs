using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.Enums;

namespace DataManagement.Domain.Errors
{
	public static class UserErrors
	{
		public static readonly Error NotFound = new(ErrorCodes.NotFound, "User not found");
		public static readonly Error AuthPassIncorrect = new(ErrorCodes.AuthPassIncorrect, "Incorrect password");
		public static readonly Error UserNameAlreadyExists = new(ErrorCodes.UserNameAlreadyExists, "User with such username already exists");
		public static readonly Error CreationFailure = new(ErrorCodes.CreationFailure, "The creation of the user wasn't successful");
		public static readonly Error DeleteUnsuccessful = new(ErrorCodes.DeleteUnsuccessful, "No user with this name was found");
		public static readonly Error NotUpdated = new(ErrorCodes.NotUpdated, "No values were updated");
	}
}
