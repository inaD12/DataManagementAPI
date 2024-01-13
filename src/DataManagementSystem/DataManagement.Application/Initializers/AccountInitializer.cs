using DataManagement.Application.Auth.PasswordManager;
using DataManagement.Domain.Abstractions;
using DataManagement.Domain.Entities;
using DataManagement.Domain.Roles;

namespace DataManagement.Application.Initializers
{
	internal class AccountInitializer : IAccountInitializer
	{
		private readonly IDBContext _dbContext;
		private readonly IPasswordManager _passwordManager;
		public AccountInitializer(IDBContext dBContext, IPasswordManager passwordManager)
		{
			_dbContext = dBContext;
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

				UserRole userRole = await _dbContext.UserRole.GetByNameAsync(Roles.Admin);

				admin.PasswordHash = _passwordManager.HashPassword(password, out string salt);
				admin.Salt = salt;
				admin.UserRoleId = userRole.Id;

				await _dbContext.User.CreateAsync(admin);
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

				_dbContext.UserRole.CreateAsync(userRole);
			}
		}
		private async Task<bool> CheckRole(string name)
		{
			UserRole? role = await _dbContext.UserRole.GetByNameAsync(name);

			if (role == null)
			 return false;
			return true;
		}

		private async Task<bool> CheckAdmin(string username)
		{
			User? user = await _dbContext.User.GetByNameAsync(username);

			if (user == null)
				return false;
			return true;
		}
	}
}
