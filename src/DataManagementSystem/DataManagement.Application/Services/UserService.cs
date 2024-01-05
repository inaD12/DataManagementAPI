using DataManagement.Application.Abstractions;
using DataManagement.Application.Auth.PasswordManager;
using DataManagement.Application.Auth.TokenManager;
using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs.Request;
using DataManagement.Domain.DTOs.Response;
using DataManagement.Domain.Entities;
using DataManagement.Domain.Errors;

namespace DataManagement.Application.Services
{
	internal class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IPasswordManager _passwordManager;
		private readonly ITokenManager _tokenManager;

		public UserService(
			IUserRepository userRepository,
			IPasswordManager passwordManager,
			ITokenManager tokenManager)
		{
			_userRepository = userRepository;
			_passwordManager = passwordManager;
			_tokenManager = tokenManager;
		}

		public async Task<ResponseDTO> LoginUser(LoginUserDTO loginDTO)
		{
			LoginResponseDTO loginResponse = new LoginResponseDTO();
			Result result = Result.Success();

			User? user = await _userRepository.GetByNameAsync(loginDTO.Username);

			if (user is null)
			{
				result = UserErrors.NotFound;
			}
			else if (!_passwordManager.VerifyPassword(loginDTO.Password, user.PasswordHash, user.Salt))
			{
				result = UserErrors.AuthPassIncorrect;
			}
			else
			{
				result = Result.Success();
				loginResponse.Token = _tokenManager.CreateToken(360);
			}

			return new ResponseDTO(result, loginResponse);
		}

		public async Task<Result> RegisterUser(RegisterUserDTO registerDTO)
		{
			User? user = await _userRepository.GetByNameAsync(registerDTO.Username);

			if (user is not null)
			{
				return UserErrors.UserNameAlreadyExists;
			}

			User newUser = new()
			{
				FirstName = registerDTO.FirstName,
				LastName = registerDTO.LastName,
				Name = registerDTO.Username,
				PasswordHash = _passwordManager.HashPassword(registerDTO.Password, out string salt),
				Salt = salt
			};

			newUser.Set();

			bool res = await _userRepository.CreateAsync(newUser);

			if (res)
			{
				return Result.Success();
			}

			return UserErrors.CreationFailure;
		}

		public async Task<ResponseDTO> GetUserByNameAsync(string userName)
		{
			Result result = Result.Success();

			User? res = await _userRepository.GetByNameAsync(userName);

			if (res is null)
			{
				result = UserErrors.NotFound;
				ResponseDTO failDTO = new ResponseDTO(result);

				return failDTO;
			}

			GetUserResponseDTO dto = new GetUserResponseDTO(res.Name, res.FirstName, res.LastName, (DateTime)res.CreatedAt);
			ResponseDTO responseDTO = new ResponseDTO(result, dto);

			return responseDTO;
		}

		public async Task<Result> UpdateUser(UpdateUserDTO updateDTO, string username)
		{
			User? user = await _userRepository.GetByNameAsync(username);

			if (user is null)
			{
				return UserErrors.NotFound;
			}

			user.FirstName = updateDTO.FirstName ?? user.FirstName;
			user.LastName = updateDTO.LastName ?? user.LastName;

			var res = await _userRepository.UpdateAsync(user);

			if (res)
			{
				return Result.Success();
			}

			return UserErrors.NotUpdated;
		}

		public async Task<Result> DeleteUser(string userName)
		{
			var res = await _userRepository.SoftDeleteByNameAsync(userName);

			if (res)
			{
				return Result.Success();
			}

			return UserErrors.DeleteUnsuccessful;
		}
	}
}
