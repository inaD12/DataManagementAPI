using Newtonsoft.Json;
using System.Text;
using System.Xml;

namespace Client
{
	class Program
	{
		static void Main(string[] args)
		{
			// Sample CSV data (replace this with your actual CSV data)
			string csvData = "Index,Organization Id,Name,Website,Country,Description,Founded,Industry,Number of employees\n" +
							  "1,FAB0d41d5b5d22c,Ferrell LLC,https://price.net/,Papua New Guinea,Horizontal empowering knowledgebase,1990,Plastics,3498";

			// Convert CSV to JSON
			string jsonData = ConvertCsvToJson(csvData);

			// Print the resulting JSON
			Console.WriteLine(jsonData);
		}

		static string ConvertCsvToJson(string csvData)
		{
			var lines = csvData.Split('\n').Select(line => line.Trim()).ToList();

			if (lines.Count < 2)
			{
				throw new InvalidOperationException("CSV data must have a header and at least one data row.");
			}

			// Skip the header (first line)
			var headers = lines[0].Split(',').Select(header => header.Trim()).ToList();

			List<Dictionary<string, string>> jsonDataList = new List<Dictionary<string, string>>();

			// Start processing from the second line
			for (int i = 1; i < lines.Count; i++)
			{
				// Skip empty lines
				if (string.IsNullOrWhiteSpace(lines[i]))
				{
					continue;
				}

				var values = lines[i].Split(',').Select(value => value.Trim()).ToList();

				if (values.Count != headers.Count)
				{
					throw new InvalidOperationException($"Mismatch between header and data row at line {i + 1}.");
				}

				var jsonDataEntry = new Dictionary<string, string>();

				for (int j = 0; j < headers.Count; j++)
				{
					jsonDataEntry[headers[j]] = values[j];
				}

				jsonDataList.Add(jsonDataEntry);
			}

			return JsonConvert.SerializeObject(jsonDataList, Newtonsoft.Json.Formatting.Indented);
		}
	}
}