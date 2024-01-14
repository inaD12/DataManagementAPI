using DataManagement.Application.Abstractions.Interfaces;
using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs.Response;
using DataManagement.Domain.DTOs.Stats;
using DataManagement.Domain.Errors;

namespace DataManagement.Application.Services
{
    internal class StatsService : IStatsService
	{
		private readonly IDBContext _dbContext;

		public StatsService(IDBContext dBContext)
		{
			_dbContext = dBContext;
		}

		public async Task<ResponseDTO> GetTopTenOrganizationsWithMostWorkers()
		{
			ICollection<OrganizationStatistics>? res = await _dbContext.Stats.GetTopTenOrganizationsWithMostWorkers();

			if (res is null)
			{
				return new ResponseDTO(StatsErrors.NotFound);
			}

			if (res.Count == 0)
			{
				return new ResponseDTO(StatsErrors.MissingData);
			}

			return new ResponseDTO(Result.Success(), res);
		}

		public async Task<ResponseDTO> GetWorkerCountByIndustries()
		{
			ICollection<IndustryCountStatistics>? res = await _dbContext.Stats.GetWorkerCountByIndustries();

			if (res is null)
			{
				return new ResponseDTO(StatsErrors.NotFound);
			}

			if (res.Count == 0)
			{
				return new ResponseDTO(StatsErrors.MissingData);
			}

			return new ResponseDTO(Result.Success(), res);
		}

		public async Task<ResponseDTO> GetTotalWorkerCount()
		{
			var res = await _dbContext.Stats.GetTotalWorkerCount();

			if (res is null)
			{
				return new ResponseDTO(StatsErrors.NotFound);
			}

			return new ResponseDTO(Result.Success(), res);
		}
	}
}
