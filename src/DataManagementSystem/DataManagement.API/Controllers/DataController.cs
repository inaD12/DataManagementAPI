using DataManagement.API.Extensions;
using DataManagement.Application.Services;
using DataManagement.Application.Services.FileServices;
using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs;
using DataManagement.Domain.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace DataManagement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DataController : Controller
	{
		private readonly IFileService _fileService;

		public DataController(IFileService fileService) 
		{
			_fileService = fileService;
		}

		[HttpPost("CSVData")]
		public async Task<IActionResult> CSVData(List<FileData> data)
		{
			_fileService.SaveData(data);

			return new OkResult();
		}
	}
}
