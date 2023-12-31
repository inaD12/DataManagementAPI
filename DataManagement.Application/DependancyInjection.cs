using DataManagement.Application.Abstractions;
using DataManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement.Application
{
	public static class DependancyInjection
	{
		public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
		{
			services.AddTransient<ICountryService, CountryService>();

			return services;
		}
	}
}
