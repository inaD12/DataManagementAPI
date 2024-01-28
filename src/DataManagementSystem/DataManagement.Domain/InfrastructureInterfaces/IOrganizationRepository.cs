using DataManagement.Domain.Entities;

namespace DataManagement.Domain.InfrastructureInterfaces
{
    public interface IOrganizationRepository : IRepository<Organization>
    {
        Task<IEnumerable<Industry>> GetIndustriesByOrganizationIdAsync(string organizationId);
    }
}