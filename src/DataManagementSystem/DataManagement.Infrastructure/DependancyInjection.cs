using DataManagement.Application.Abstractions.Interfaces;
using DataManagement.Infrastructure.Data;
using DataManagement.Infrastructure.DBContexts;
using DataManagement.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataManagement.Infrastructure
{
    public static class DependancyInjection
	{
		public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
		{
			services.AddTransient<ISqlConnectionFactory, SqlConnectionFactory>();
			services.AddTransient<IRepositoryFactory, RepositoryFactory>();
			services.AddTransient<IRepositoryHelper, RepositoryHelper>();
			services.AddTransient<ICountryRepository, CountryRepository>();
			services.AddTransient<IIndustryRepository, IndustryRepository>();
			services.AddTransient<IOrganizationRepository, OrganizationRepository>();
			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<IStatsRepository, StatsRepository>();
			services.AddTransient<IUserRoleRepository, UserRoleRepository>();
			services.AddTransient<IFullDataRepository, FullDataRepository>();
			services.AddTransient<IDBContext, DBContext>();


			return services;
		}
	}
}
