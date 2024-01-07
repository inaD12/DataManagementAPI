using DataManagement.API.Extensions;
using DataManagement.Application.Services;
using DataManagement.Domain.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace DataManagement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StatsController : Controller
	{
		private readonly IStatsService _statsService;

		public StatsController(IStatsService statsService)
		{
			_statsService = statsService;
		}

		[HttpGet("GetTopTenOrganizationsWithMostWorkers")]
		public async Task<IActionResult> GetTopTenOrganizationsWithMostWorkers()
		{
			ResponseDTO response = await _statsService.GetTopTenOrganizationsWithMostWorkers();

			if (!response.Result.IsSuccess)
			{
				return this.ParseAndReturnMessage(response.Result);
			}

			return Ok(response.obj);
		}

		[HttpGet("GetWorkerCountByIndustries")]
		public async Task<IActionResult> GetWorkerCountByIndustries()
		{
			ResponseDTO response = await _statsService.GetWorkerCountByIndustries();

			if (!response.Result.IsSuccess)
			{
				return this.ParseAndReturnMessage(response.Result);
			}

			return Ok(response.obj);
		}

		[HttpGet("GetTotalWorkerCount")]
		public async Task<IActionResult> GetTotalWorkerCount()
		{
			ResponseDTO response = await _statsService.GetTotalWorkerCount();

			if (!response.Result.IsSuccess)
			{
				return this.ParseAndReturnMessage(response.Result);
			}

			return Ok(response.obj);
		}
	}
}
