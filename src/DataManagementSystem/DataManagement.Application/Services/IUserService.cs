using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs.Request;
using DataManagement.Domain.DTOs.Response;

namespace DataManagement.Application.Services
{
	public interface IUserService
	{
		Task<Result> DeleteUser(string userName);
		Task<ResponseDTO> GetUserByNameAsync(string userName);
		Task<ResponseDTO> LoginUser(LoginUserDTO loginDTO);
		Task<Result> RegisterUser(RegisterUserDTO registerDTO);
		Task<Result> UpdateUser(UpdateUserDTO updateDTO, string username);
	}
}