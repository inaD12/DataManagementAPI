using Microsoft.AspNetCore.Mvc;
using DataManagement.Application.PDF;
using DataManagement.API.Extensions;

namespace DataManagement.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PdfController : ControllerBase
	{
		private readonly IPDFGenerator _pdfGenerator;

		public PdfController(IPDFGenerator pdfGenerator)
		{
			_pdfGenerator = pdfGenerator;
		}

		[HttpGet("GeneratePdf/{organizationName}")]
		[Produces("application/pdf")]
		public async Task<IActionResult> GeneratePdf(string organizationName)
		{
			var response = await _pdfGenerator.GeneratePDF(organizationName);

			if (!response.Result.IsSuccess)
			{
				return this.ParseAndReturnMessage(response.Result);
			}

			byte[] pdfBytes = (byte[])response.obj;
			var result = new FileContentResult(pdfBytes, "application/pdf");
			result.FileDownloadName = "RequestedData.pdf";

			return result;
		}
	}
}
