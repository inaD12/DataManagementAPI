using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs.Request;
using DataManagement.Domain.DTOs.Response;

namespace DataManagement.Application.Services
{
	public interface ICountryService
	{
		Task<ResponseDTO> GetCountryByNameAsync(string countryName);

		Task<Result> CreateCountryAsync(CreateCountryRequestDTO dto);
		Task<Result> DeleteCountryAsync(string countryName);
		Task<Result> UpdateCountryAsync(UpdateCountryRequestDTO dto, string countryName);
	}
}