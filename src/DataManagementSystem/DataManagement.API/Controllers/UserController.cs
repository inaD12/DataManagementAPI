using DataManagement.API.Extensions;
using DataManagement.Application.Abstractions.Interfaces;
using DataManagement.Application.Abstractions.Interfaces.Services;
using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs.Request;
using DataManagement.Domain.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataManagement.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class UserController : Controller
	{
		private readonly IUserService _userService;
		private readonly IJWTParser _jwtParser;

		public UserController(IUserService userService, IJWTParser jWTParser)
		{
			_userService = userService;
			_jwtParser = jWTParser;
		}

		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> Register(RegisterUserDTO userDTO)
		{
			Result result = await _userService.RegisterUser(userDTO);

			if (!result.IsSuccess)
			{
				return this.ParseAndReturnMessage(result);
			}

			return Created($"/api/User/{userDTO.Username}", userDTO);
		}

		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login(LoginUserDTO userDTO)
		{
			ResponseDTO response = await _userService.LoginUser(userDTO);

			if (!response.Result.IsSuccess)
			{
				return this.ParseAndReturnMessage(response.Result);
			}

			return Ok(response.obj);
		}

		[Authorize]
		[HttpGet]
		[Route("UserInformation")]
		public async Task<IActionResult> UserInformation()
		{
			string username = _jwtParser.GetUsernameFromToken();

			ResponseDTO response = await _userService.GetUserByNameAsync(username);

			if (!response.Result.IsSuccess)
			{
				return this.ParseAndReturnMessage(response.Result);
			}

			return Ok(response.obj);
		}

		[Authorize]
		[HttpPut]
		[Route("UpdateUser")]
		public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUserDTO)
		{
			string username = _jwtParser.GetUsernameFromToken();

			Result result = await _userService.UpdateUser(updateUserDTO, username);

			if (!result.IsSuccess)
			{
				return this.ParseAndReturnMessage(result);
			}

			return Ok(result.Error);
		}

		[Authorize(Policy = "RequireAdminRole")]
		[HttpDelete]
		[Route("DeleteUser/{username}")]
		public async Task<IActionResult> DeleteUser(string username)
		{
			Result result = await _userService.DeleteUser(username);

			if (!result.IsSuccess)
			{
				return this.ParseAndReturnMessage(result);
			}

			return Ok();
		}
	}
}
