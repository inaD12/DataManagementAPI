using DataManagement.Domain.DTOs.Stats;

namespace DataManagement.Domain.InfrastructureInterfaces
{
    public interface IStatsRepository
    {
        Task<ICollection<OrganizationStatistics>?> GetTopTenOrganizationsWithMostWorkers();
        Task<string?> GetTotalWorkerCount();
        Task<ICollection<IndustryCountStatistics>?> GetWorkerCountByIndustries();
    }
}