using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs.Request;

namespace DataManagement.Application.Abstractions.Interfaces.Services
{
    public interface IIndustryOrganizationService
    {
        Task<Result> CreateAsync(IndustryOrganizationRequestDTO dto);

        Task<Result> DeleteAsync(IndustryOrganizationRequestDTO dto);

    }
}