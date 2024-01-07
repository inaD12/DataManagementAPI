using DataManagement.Domain.DTOs.Response;

namespace DataManagement.Application.Services
{
	public interface IStatsService
	{
		Task<ResponseDTO> GetTopTenOrganizationsWithMostWorkers();
		Task<ResponseDTO> GetTotalWorkerCount();
		Task<ResponseDTO> GetWorkerCountByIndustries();
	}
}