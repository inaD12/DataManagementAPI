using DataManagement.Application.Abstractions;
using DataManagement.Application.Auth.PasswordManager;
using DataManagement.Application.Auth.TokenManager;
using DataManagement.Application.Initializers;
using DataManagement.Application.Services;
using DataManagement.Application.Services.FileServices;
using DataManagement.Application.Services.FileServices.Data;
using DataManagement.Application.Services.IndustryOrganizationServices;
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
			services.AddTransient<IFileService, FileService>();
			services.AddTransient<INormalizer, Normalizer>();
			services.AddTransient<IInserter, Inserter>();
			services.AddTransient<IEntityFactory, EntityFactory>();
			services.AddTransient<IDBCreator, DBCreator>();
			services.AddSingleton<ITableCreator, TableCreator>();

			return services;
		}
	}
}
