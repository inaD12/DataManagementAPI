using DataManagement.Domain.Entities;

namespace DataManagement.Application.Abstractions.Interfaces
{
    public interface IOrganizationRepository : IRepository<Organization>
    {
        Task<IEnumerable<Industry>> GetIndustriesByOrganizationIdAsync(string organizationId);
    }
}