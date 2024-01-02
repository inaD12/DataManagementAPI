using DataManagement.Application.Abstractions;
using DataManagement.Infrastructure.Data;
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


			return services;
		}
	}
}
