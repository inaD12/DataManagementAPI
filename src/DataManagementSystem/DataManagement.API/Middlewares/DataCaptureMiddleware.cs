using Serilog;

namespace DataManagement.API.Middlewares
{
	public class DataCaptureMiddleware
	{
		private readonly RequestDelegate _next;

		public DataCaptureMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			if (context.Request.Path.Equals("/api/Data/CSVData", StringComparison.OrdinalIgnoreCase)
			&& context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
			{
				var requestData = await CaptureData(context.Request);

				WriteDataToJsonFile(requestData);
			}

			await _next(context);
		}

		private async Task<string> CaptureData(HttpRequest request)
		{
			try
			{
				var clonedStream = new MemoryStream();
				await request.Body.CopyToAsync(clonedStream);
				clonedStream.Seek(0, SeekOrigin.Begin);

				using (var reader = new StreamReader(clonedStream))
				{
					return await reader.ReadToEndAsync();
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error in DataCaptureMiddleware, CaptureData: {ex.Message}");
				return string.Empty;
			}
			finally
			{
				request.Body.Seek(0, SeekOrigin.Begin);
			}
		}

		private void WriteDataToJsonFile(string data)
		{
			try
			{
				var directoryPath = "../DailyJsonStatistics";
				var fileName = $"{DateTime.Now:ddMMyyyy}.json";
				var filePath = Path.Combine(directoryPath, fileName);

				if (!File.Exists(filePath))
				{
					File.WriteAllText(filePath, "[]");
				}

				File.AppendAllText(filePath, Environment.NewLine + data);
			}
			catch (Exception ex)
			{
				Log.Error($"Error in DataCaptureMiddleware, WriteDataToJsonFile: {ex.Message}");
			}
		}
	}
}
