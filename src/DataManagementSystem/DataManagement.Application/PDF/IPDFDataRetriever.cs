using DataManagement.Domain.DTOs.Response;

namespace DataManagement.Application.PDF
{
	public interface IPDFDataRetriever
	{
		Task<ResponseDTO> Retrieve(string OrganizationName);
	}
}