using DataManagement.Domain.Entities;

namespace DataManagement.Application.Abstractions.Interfaces
{
    public interface IIndustryOrganizationRepository
    {
        Task<bool> CreateAsync(IndustryOrganization entity);
        Task<bool> DeleteAsync(IndustryOrganization entity);
    }
}