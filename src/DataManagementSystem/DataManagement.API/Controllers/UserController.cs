using DataManagement.API.Extensions;
using DataManagement.Application.Services;
using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs.Request;
using DataManagement.Domain.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace DataManagement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : Controller
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
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

			return Ok(response);
		}

		[HttpGet]
		[Route("UserInformation/{username}")]
		public async Task<IActionResult> UserInformation(string username)
		{
			ResponseDTO response = await _userService.GetUserByNameAsync(username);

			if (!response.Result.IsSuccess)
			{
				return this.ParseAndReturnMessage(response.Result);
			}

			return Ok(response);
		}

		[HttpPut]
		[Route("UpdateUser/{username}")]
		public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUserDTO, [FromRoute] string username)
		{
			Result result = await _userService.UpdateUser(updateUserDTO, username);

			if (!result.IsSuccess)
			{
				return this.ParseAndReturnMessage(result);
			}

			return Ok(result);
		}

		[HttpDelete]
		[Route("DeleteUser/{username}")]
		public async Task<IActionResult> DeleteUser(string username)
		{
			Result result = await _userService.DeleteUser(username);

			if (!result.IsSuccess)
			{
				return this.ParseAndReturnMessage(result);
			}

			return Ok(result);
		}
	}
}
