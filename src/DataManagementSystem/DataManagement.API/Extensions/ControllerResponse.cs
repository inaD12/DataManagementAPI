using DataManagement.Domain.Abstractions.Result;
using Microsoft.AspNetCore.Mvc;

namespace DataManagement.API.Extensions
{
	public static class ControllerResponse
	{
		public static IActionResult ParseAndReturnMessage(this ControllerBase controller, Result result)
		{
			ResponseType responseType = ResponseParser.ParseResponse(result.Error);

			switch (responseType)
			{
				case ResponseType.NotFound:
					return controller.NotFound(result.Error.Description);
				case ResponseType.Unauthorized:
					return controller.Unauthorized(result.Error.Description);
				case ResponseType.Conflict:
					return controller.Conflict(result.Error.Description);
				case ResponseType.BadRequest:	
					return controller.BadRequest(result.Error.Description);
				default:
					return controller.Ok(result);
			}
		}
	}
}
