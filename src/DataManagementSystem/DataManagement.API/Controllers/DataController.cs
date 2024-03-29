﻿using DataManagement.Application.Abstractions.Interfaces.Services;
using DataManagement.Domain.DTOs;
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
			await _fileService.SaveData(data);

			return new OkResult();
		}
	}
}
