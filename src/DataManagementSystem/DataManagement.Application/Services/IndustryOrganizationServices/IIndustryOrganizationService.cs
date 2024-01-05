using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs.Request;

namespace DataManagement.Application.Services.IndustryOrganizationServices
{
	public interface IIndustryOrganizationService
    {
        Task<Result> CreateAsync(IndustryOrganizationRequestDTO dto);

        Task<Result> DeleteAsync(IndustryOrganizationRequestDTO dto);

	}
}