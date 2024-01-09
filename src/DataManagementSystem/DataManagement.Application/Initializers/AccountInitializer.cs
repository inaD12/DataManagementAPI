using DataManagement.Application.Abstractions;
using DataManagement.Application.Auth.PasswordManager;
using DataManagement.Domain.DTOs;
using DataManagement.Domain.Entities;
using DataManagement.Domain.Roles;
using System.Data;
using System.Net;

namespace DataManagement.Application.Initializers
{
	internal class AccountInitializer : IAccountInitializer
	{
		private readonly IUserRepository _userRepository;
		private readonly IUserRoleRepository _userRoleRepository;
		private readonly IPasswordManager _passwordManager;
		public AccountInitializer(IRepositoryFactory repositoryFactory, IPasswordManager passwordManager)
		{
			_userRepository = repositoryFactory.CreateUserRepository();
			_userRoleRepository = repositoryFactory.CreateUserRoleRepository();
			_passwordManager = passwordManager;
		}


		public void TryCreate()
		{
			CreateRoleIfItDoesntExist(Roles.Admin);
			CreateRoleIfItDoesntExist(Roles.User);

			CreateAdminIfItDoesntExist("admin", "password");
		}
		private async Task CreateAdminIfItDoesntExist(string username, string password)
		{
			if (await CheckAdmin(username) == false)
			{
				User admin = new User()
				{
					Name = username,
				};

				admin.Set();

				UserRole userRole = await _userRoleRepository.GetByNameAsync(Roles.Admin);

				admin.PasswordHash = _passwordManager.HashPassword(password, out string salt);
				admin.Salt = salt;
				admin.UserRoleId = userRole.Id;

				await _userRepository.CreateAsync(admin);
			}
		}
		private async Task CreateRoleIfItDoesntExist(string name)
		{
			if (await CheckRole(name) == false)
			{
				UserRole userRole = new UserRole()
				{
					Name = name,
				};

				userRole.Set();

				_userRoleRepository.CreateAsync(userRole);
			}
		}
		private async Task<bool> CheckRole(string name)
		{
			UserRole? role = await _userRoleRepository.GetByNameAsync(name);

			if (role == null)
			 return false;
			return true;
		}

		private async Task<bool> CheckAdmin(string username)
		{
			User? user = await _userRepository.GetByNameAsync(username);

			if (user == null)
				return false;
			return true;
		}
	}
}
