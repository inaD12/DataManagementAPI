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
					page.PageColor(Colors.White);
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
					column.Item().Text($"{data.Name} details").Style(titleStyle);
				});

				row.ConstantItem(100).Height(50).Placeholder();
			});
		}

		private static void ComposeContent(IContainer container, FileData data)
		{
			container.Table(table =>
			{
				table.ColumnsDefinition(columns =>
				{
					columns.RelativeColumn();
					columns.RelativeColumn();
					columns.RelativeColumn();
					columns.RelativeColumn();
					columns.RelativeColumn();
					columns.RelativeColumn();
					columns.RelativeColumn();
					columns.RelativeColumn();
				});
				table.Header(header =>
				{
					header.Cell().Element(CellStyle).Text("Organization Id");
					header.Cell().Element(CellStyle).Text("Name");
					header.Cell().Element(CellStyle).Text("Website");
					header.Cell().Element(CellStyle).Text("Country");
					header.Cell().Element(CellStyle).Text("Description");
					header.Cell().Element(CellStyle).Text("Founded");
					header.Cell().Element(CellStyle).Text("Industry");
					header.Cell().Element(CellStyle).Text("Number Of Employees");

					static IContainer CellStyle(IContainer container)
					{
						return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
					}
				});

				table.Cell().Element(CellStyle).Text(data.OrganizationId);
				table.Cell().Element(CellStyle).Text(data.Name);
				table.Cell().Element(CellStyle).Text(data.Website);
				table.Cell().Element(CellStyle).Text(data.Country);
				table.Cell().Element(CellStyle).Text(data.Description);
				table.Cell().Element(CellStyle).Text(data.Founded);
				table.Cell().Element(CellStyle).Text(data.Industry);
				table.Cell().Element(CellStyle).Text(data.NumberOfEmployees);


				static IContainer CellStyle(IContainer container)
				{
					return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
				}

			});
		}
	}
}

