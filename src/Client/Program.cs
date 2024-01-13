using Newtonsoft.Json;
using System.Text;

class Program
{
	static async Task Main(string[] args)
	{
		string apiUrl = "";
		string csvFilePath = "C:\\Users\\Capit\\OneDrive\\Работен плот\\organizations-100.csv";

		try
		{
			string csvData = File.ReadAllText(csvFilePath);

			string jsonData = ConvertCsvToJson(csvData);

			await SendDataToApi(apiUrl, jsonData);

			Console.WriteLine("Data sent successfully.");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error: {ex.Message}");
		}
	}

	static string ConvertCsvToJson(string csvData)
	{
		var lines = csvData.Split('\n').Select(line => line.Trim()).ToList();

		if (lines.Count < 2)
		{
			throw new InvalidOperationException("CSV data must have a header and at least one data row.");
		}

		var headers = lines[0].Split(',').Select(header => header.Trim()).Where(header => header != "Index").ToList();

		List<Dictionary<string, string>> jsonDataList = new List<Dictionary<string, string>>();

		for (int i = 1; i < lines.Count; i++)
		{
			if (string.IsNullOrWhiteSpace(lines[i]))
			{
				continue;
			}

			var values = lines[i].Split(',').Skip(1);

			if (values.Count() < headers.Count)
			{
				Console.WriteLine($"Warning: Insufficient data at line {i + 1}. Skipping this row.");
				continue;
			}

			var jsonDataEntry = new Dictionary<string, string>();

			for (int j = 0; j < headers.Count; j++)
			{
				jsonDataEntry[headers[j]] = values.ElementAt(j).Trim();
			}

			jsonDataList.Add(jsonDataEntry);
		}

		return JsonConvert.SerializeObject(jsonDataList, Formatting.Indented);
	}

	static async Task SendDataToApi(string apiUrl, string jsonData)
	{
		using (HttpClient client = new HttpClient())
		{
			StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await client.PostAsync(apiUrl, content);

			response.EnsureSuccessStatusCode();
		}
	}
}
