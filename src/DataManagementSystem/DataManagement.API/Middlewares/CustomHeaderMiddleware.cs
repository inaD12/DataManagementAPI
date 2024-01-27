namespace DataManagement.API.Middlewares
{
	public class CustomHeaderMiddleware
	{
		private readonly RequestDelegate _next;

		public CustomHeaderMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			context.Response.Headers.Add("Header", "DataManagementAPI");

			await _next(context);
		}
	}
}
