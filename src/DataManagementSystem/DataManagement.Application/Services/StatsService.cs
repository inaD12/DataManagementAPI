using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs.Response;
using DataManagement.Domain.DTOs.Stats;
using DataManagement.Domain.Errors;
using DataManagement.Infrastructure.Repositories;

namespace DataManagement.Application.Services
{
	internal class StatsService : IStatsService
	{
		private readonly IStatsRepository _statsRepository;

		public StatsService(IStatsRepository statsRepository)
		{
			_statsRepository = statsRepository;
		}

		public async Task<ResponseDTO> GetTopTenOrganizationsWithMostWorkers()
		{
			ICollection<OrganizationStatistics>? res = await _statsRepository.GetTopTenOrganizationsWithMostWorkers();

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
			ICollection<IndustryCountStatistics>? res = await _statsRepository.GetWorkerCountByIndustries();

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
			var res = await _statsRepository.GetTotalWorkerCount();

			if (res is null)
			{
				return new ResponseDTO(StatsErrors.NotFound);
			}

			return new ResponseDTO(Result.Success(), res);
		}
	}
}
