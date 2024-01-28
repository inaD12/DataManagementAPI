using DataManagement.Application.Abstractions.Interfaces;
using DataManagement.Domain.Entities;
using DataManagement.Domain.InfrastructureInterfaces;
using DataManagement.Domain.Roles;
using Serilog;

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


		public async Task TryCreate()
		{
			await CreateRoleIfItDoesntExist(Roles.Admin);
			await CreateRoleIfItDoesntExist(Roles.User);

			await CreateAdminIfItDoesntExist("admin", "password");
		}
		private async Task CreateAdminIfItDoesntExist(string username, string password)
		{
			try
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
			catch (Exception ex)
			{
				Log.Error($"Error in AccountInitializer, CreateAdminIfItDoesntExist: {ex.Message}");
			}
		}
		private async Task CreateRoleIfItDoesntExist(string name)
		{
			try
			{
				if (await CheckRole(name) == false)
				{
					UserRole userRole = new UserRole()
					{
						Name = name,
					};

					userRole.Set();

					await _dbContext.UserRole.CreateAsync(userRole);
				}
			}
			catch (Exception ex)
			{
				Log.Error($"Error in AccountInitializer, CreateRoleIfItDoesntExist: {ex.Message}");
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
