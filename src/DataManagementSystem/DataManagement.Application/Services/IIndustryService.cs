using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs.Request;
using DataManagement.Domain.DTOs.Response;

namespace DataManagement.Application.Services
{
	public interface IIndustryService
	{
		Task<ResponseDTO> GetIndustryByNameAsync(string industryName);
		Task<Result> CreateIndustryAsync(CreateIndustryRequestDTO dto);
		Task<Result> DeleteIndustryAsync(string industryName);
		Task<Result> UpdateIndustryAsync(UpdateIndustryRequestDTO dto, string industryName);
	}
}