using DataManagement.Domain.DTOs.Stats;

namespace DataManagement.Infrastructure.Repositories
{
	public interface IStatsRepository
	{
		Task<ICollection<OrganizationStatistics>?> GetTopTenOrganizationsWithMostWorkers();
		Task<string> GetTotalWorkerCount();
		Task<ICollection<IndustryCountStatistics>?> GetWorkerCountByIndustries();
	}
}