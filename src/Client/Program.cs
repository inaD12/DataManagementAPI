﻿using Newtonsoft.Json;
using System.Text;

class Program
{
	private static string apiUrl = "https://localhost:7174/api/Data/CSVData";
	private static string inputFolder = "..\\..\\..\\InputFolder";
	private static string outputFolder = "..\\..\\..\\OutputFolder";

	static async Task Main(string[] args)
	{
		using (FileSystemWatcher watcher = new FileSystemWatcher(inputFolder))
		{
			watcher.Filter = "*.csv";
			watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;

			watcher.Created += async (sender, e) =>
			{
				Console.WriteLine($"File {e.Name} created. Processing...");

				await ProcessCsvFile(e.FullPath);

				Console.WriteLine($"File {e.Name} processed and moved to output folder.");
			};

			watcher.EnableRaisingEvents = true;

			Console.WriteLine("Press Enter to exit.");
			Console.ReadLine();
		}
	}

	static async Task ProcessCsvFile(string csvFilePath)
	{
		try
		{
			string csvData = File.ReadAllText(csvFilePath);

			string jsonData = ConvertCsvToJson(csvData);

			await SendDataToApi(apiUrl, jsonData);

			string outputFilePath = Path.Combine(outputFolder, Path.GetFileName(csvFilePath));
			File.Move(csvFilePath, outputFilePath);

			Console.WriteLine($"Data sent successfully. Moved {csvFilePath} to {outputFilePath}");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error processing file {csvFilePath}: {ex.Message}");
		}
	}

	static string ConvertCsvToJson(string csvData)
	{
		var lines = csvData.Split('\n').Select(line => line.Trim()).ToList();

		if (lines.Count < 2)
		{
			throw new InvalidOperationException("CSV data must have a header and at least one data row.");
		}

		var headers = lines[0].Split(',').Select(header => header.Trim()).ToList();

		if (headers.Contains("Index"))
		{
			headers.Remove("Index");
		}

		if (headers.Contains("Organization Id"))
		{
			int index = headers.IndexOf("Organization Id");
			headers[index] = "OrganizationId";
		}
		if (headers.Contains("Number of employees"))
		{
			int index = headers.IndexOf("Number of employees");
			headers[index] = "NumberOfEmployees";
		}

		List<Dictionary<string, string>> jsonDataList = new List<Dictionary<string, string>>();

		for (int i = 1; i < lines.Count; i++)
		{
			if (string.IsNullOrWhiteSpace(lines[i]))
			{
				continue;
			}

			var values = ParseCsvLine(lines[i]);

			if (values.Count < headers.Count)
			{
				Console.WriteLine($"Warning: Insufficient data at line {i + 1}. Skipping this row.");
				continue;
			}

			var jsonDataEntry = headers.Zip(values, (header, value) => new { header, value })
									  .ToDictionary(pair => pair.header, pair => pair.value.Trim());

			jsonDataList.Add(jsonDataEntry);
		}

		return JsonConvert.SerializeObject(jsonDataList, Formatting.Indented);
	}

	static List<string> ParseCsvLine(string csvLine)
	{
		var values = new List<string>();
		var inQuotes = false;
		var currentValue = new StringBuilder();

		foreach (char c in csvLine)
		{
			if (c == '"')
			{
				inQuotes = !inQuotes;
			}
			else if (c == ',' && !inQuotes)
			{
				values.Add(currentValue.ToString().Trim(' ', '"'));
				currentValue.Clear();
			}
			else
			{
				currentValue.Append(c);
			}
		}

		values.Add(currentValue.ToString().Trim(' ', '"'));

		return values.Skip(1).ToList();
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
