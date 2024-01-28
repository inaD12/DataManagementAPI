using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs.Request;
using DataManagement.Domain.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace DataManagement.Application.Abstractions.Interfaces.Services
{
    public interface IOrganizationService
    {
        Task<ResponseDTO> GetOrganizationByNameAsync(string OrganizationName);
        Task<Result> CreateOrganizationAsync(CreateOrganizationRequestDTO dto);
        Task<Result> DeleteOrganizationAsync(string OrganizationName);
        Task<Result> UpdateOrganizationAsync(UpdateOrganizationRequestDTO dto, string organizationName);
    }
}