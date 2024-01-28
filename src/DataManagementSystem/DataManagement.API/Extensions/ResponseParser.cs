using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.Enums;

namespace DataManagement.API.Extensions
{

	public static class ResponseParser
	{
		public static ResponseType ParseResponse(Error error)
		{
			switch (error.Code)
			{
				case ErrorCodes.NotFound:
					return ResponseType.NotFound;

					//return ResponseType.Unauthorized;

				case ErrorCodes.AuthPassIncorrect:
				case ErrorCodes.UserNameAlreadyExists:
				case ErrorCodes.NotUpdated:
					return ResponseType.Conflict;

				case ErrorCodes.CreationFailure:
				case ErrorCodes.DeleteUnsuccessful:
					return ResponseType.BadRequest;

				default:
					return ResponseType.Success;
			}
		}
	}
}
