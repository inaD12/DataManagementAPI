using DataManagement.Domain.DTOs.Response;

namespace DataManagement.Application.Abstractions.Interfaces
{
    public interface IPDFGenerator
    {
        Task<ResponseDTO> GeneratePDF(string organizationName);
    }
}