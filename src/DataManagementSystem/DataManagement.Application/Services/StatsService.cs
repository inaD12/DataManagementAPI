using DataManagement.Application.Abstractions.Interfaces;
using DataManagement.Application.Helpers;
using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs.Response;
using DataManagement.Domain.DTOs.Stats;
using DataManagement.Domain.Errors;

namespace DataManagement.Application.Services
{
	internal class StatsService : IStatsService
	{
		private readonly IDBContext _dbContext;
		private readonly ICacheHelper _cacheHelper;

		public StatsService(IDBContext dbContext, ICacheHelper cacheHelper)
		{
			_dbContext = dbContext;
			_cacheHelper = cacheHelper;
		}

		public async Task<ResponseDTO> GetTopTenOrganizationsWithMostWorkers()
		{
			return await GetFromCacheOrServiceAsync<ICollection<OrganizationStatistics>>(
				cacheKey: "TopTenOrganizationsWithMostWorkers",
				serviceMethod: _dbContext.Stats.GetTopTenOrganizationsWithMostWorkers
			);
		}

		public async Task<ResponseDTO> GetWorkerCountByIndustries()
		{
			return await GetFromCacheOrServiceAsync<ICollection<IndustryCountStatistics>>(
				cacheKey: "WorkerCountByIndustries",
				serviceMethod: _dbContext.Stats.GetWorkerCountByIndustries
			);
		}

		public async Task<ResponseDTO> GetTotalWorkerCount()
		{
			return await GetFromCacheOrServiceAsync<string>(
				cacheKey: "TotalWorkerCount",
				serviceMethod: _dbContext.Stats.GetTotalWorkerCount
			);
		}

		private async Task<ResponseDTO> GetFromCacheOrServiceAsync<T>(string cacheKey, Func<Task<T>> serviceMethod)
		{
			T? cachedData = _cacheHelper.Get<T>(cacheKey);

			if (cachedData != null)
			{
				return new ResponseDTO(Result.Success(), cachedData);
			}

			T? result = await serviceMethod();

			if (result == null)
			{
				return new ResponseDTO(StatsErrors.NotFound);
			}
			if (typeof(T) != typeof(string))
			{
				dynamic collection = result;
				if (collection.Count == 0)
				{
					return new ResponseDTO(StatsErrors.MissingData);
				}
			}

			_cacheHelper.Set(cacheKey, result, AbsoluteExpInM: 5, SlidingExpInM: 2);

			return new ResponseDTO(Result.Success(), result);
		}
	}
}
