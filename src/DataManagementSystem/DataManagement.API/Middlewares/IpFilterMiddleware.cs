using System.Net;

namespace DataManagement.API.Middlewares
{
	public class IpFilterMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly string _allowedIp;

		public IpFilterMiddleware(RequestDelegate next, IConfiguration configuration)
		{
			_next = next;
			_allowedIp = configuration["AllowedIp"];
		}

		public async Task Invoke(HttpContext context)
		{
			var clientIp = context.Connection.RemoteIpAddress;

			if (clientIp?.ToString() != _allowedIp)
			{
				context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return;
			}

			await _next(context);
		}
	}
}
