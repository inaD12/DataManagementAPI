using DataManagement.Application.Services;
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

			return services;
		}
	}
}
