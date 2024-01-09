using DataManagement.Application.Auth.PasswordManager;
using DataManagement.Application.Auth.TokenManager;
using DataManagement.Application.Initializers;
using DataManagement.Application.Services;
using DataManagement.Application.Services.IndustryOrganizationServices;
using DataManagement.Application.Settings.Options;
using Microsoft.Extensions.DependencyInjection;

namespace DataManagement.Application
{
	public static class DependancyInjection
	{
		public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
		{
			services.AddTransient<ICountryService, CountryService>();
			services.AddTransient<IIndustryService, IndustryService>();
			services.AddTransient<IOrganizationService, OrganizationService>();
			services.AddTransient<IIndustryOrganizationService, IndustryOrganizationService>();
			services.AddTransient<IIndustryOrganizationHelper, IndustryOrganizationHelper>();
			services.AddSingleton<IPasswordManager, PasswordManager>();
			services.AddSingleton<ITokenManager, TokenManager>();
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IStatsService, StatsService>();
			services.AddTransient<IAccountInitializer, AccountInitializer>();

			return services;
		}
	}
}
