using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs;
using DataManagement.Domain.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace DataManagement.Application.PDF
{
	public class PDFGenerator : IPDFGenerator
	{
		private readonly IPDFDataRetriever _dataRetriever;

		public PDFGenerator(IPDFDataRetriever dataRetriever)
		{
			_dataRetriever = dataRetriever;
		}

		public async Task<ResponseDTO> GeneratePDF(string organizationName)
		{
			QuestPDF.Settings.License = LicenseType.Community;

			ResponseDTO res = await _dataRetriever.Retrieve(organizationName);

			if (res.Result.IsFailure)
			{
				return new ResponseDTO(res.Result);
			}

			FileData data = (FileData)res.obj;

			var doc = Document.Create(container =>
			{
				container.Page(page =>
				{
					page.Size(PageSizes.A4);
					page.PageColor(Colors.Grey.Lighten2);
					page.Header().Element(container => ComposeHeader(container, data));
					page.Content().Element(container => ComposeContent(container, data));
				});
			}).GeneratePdf();


			Result result = Result.Success();

			return new ResponseDTO(result, doc);
		}

		void ComposeHeader(IContainer container, FileData data)
		{
			var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

			container.Row(row =>
			{
				row.RelativeItem().Column(column =>
				{
					column.Item().Height(70).PaddingLeft(20).Text($"{data.Name} details").Style(titleStyle);
				});
			});
		}

		private static void ComposeContent(IContainer container, FileData data)
		{
			var titleStyle = TextStyle.Default.FontSize(17).SemiBold().FontColor(Colors.Blue.Medium);

			container.Column(column =>
			{
				column.Spacing(10);

				column.Item().Background(Colors.Grey.Lighten1).Height(50).PaddingLeft(10).Text($"Organization Id: {data.OrganizationId}").FontSize(17);
				column.Item().Background(Colors.Grey.Lighten1).Height(50).PaddingLeft(10).Text($"Name: {data.Name}").FontSize(17);
				column.Item().Background(Colors.Grey.Lighten1).Height(50).PaddingLeft(10).Text($"Website: {data.Website}").FontSize(17);
				column.Item().Background(Colors.Grey.Lighten1).Height(50).PaddingLeft(10).Text($"Country: {data.Country}").FontSize(17);
				column.Item().Background(Colors.Grey.Lighten1).Height(50).PaddingLeft(10).Text($"Description: {data.Description}").FontSize(17);
				column.Item().Background(Colors.Grey.Lighten1).Height(50).PaddingLeft(10).Text($"Founded: {data.Founded}").FontSize(17);
				column.Item().Background(Colors.Grey.Lighten1).Height(50).PaddingLeft(10).Text($"Industry: {data.Industry}").FontSize(17);
				column.Item().Background(Colors.Grey.Lighten1).Height(50).PaddingLeft(10).Text($"Number Of Employees: {data.NumberOfEmployees}").FontSize(17);
			});
		}
	}
}

