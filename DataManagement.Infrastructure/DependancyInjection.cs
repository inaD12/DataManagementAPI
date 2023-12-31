using DataManagement.Application.Abstractions;
using DataManagement.Infrastructure.Data;
using DataManagement.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


			return services;
		}
	}
}
