using DataManagement.Domain.DTOs.Response;

namespace DataManagement.Application.PDF
{
	public interface IPDFGenerator
	{
		Task<ResponseDTO> GeneratePDF(string organizationName);
	}
}