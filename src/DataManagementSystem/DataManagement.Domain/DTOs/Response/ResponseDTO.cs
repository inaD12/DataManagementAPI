using DataManagement.Domain.Abstractions.Result;

namespace DataManagement.Domain.DTOs.Response
{
	public record ResponseDTO(Result Result, object obj = null);
}
