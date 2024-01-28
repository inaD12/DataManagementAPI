using DataManagement.Domain.DTOs.Response;

namespace DataManagement.Application.Abstractions.Interfaces
{
    public interface IPDFDataRetriever
    {
        Task<ResponseDTO> Retrieve(string OrganizationName);
    }
}