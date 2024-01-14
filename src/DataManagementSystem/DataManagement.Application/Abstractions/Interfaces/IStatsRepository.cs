using DataManagement.Domain.DTOs.Stats;

namespace DataManagement.Application.Abstractions.Interfaces
{
    public interface IStatsRepository
    {
        Task<ICollection<OrganizationStatistics>?> GetTopTenOrganizationsWithMostWorkers();
        Task<string> GetTotalWorkerCount();
        Task<ICollection<IndustryCountStatistics>?> GetWorkerCountByIndustries();
    }
}