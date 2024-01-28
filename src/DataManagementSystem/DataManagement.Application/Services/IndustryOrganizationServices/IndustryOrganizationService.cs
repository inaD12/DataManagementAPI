using DataManagement.Application.Abstractions.Interfaces.Services;
using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs.Request;
using DataManagement.Domain.Errors;
using DataManagement.Domain.InfrastructureInterfaces;

namespace DataManagement.Application.Services.IndustryOrganizationServices
{
	internal class IndustryOrganizationService : IIndustryOrganizationService
	{
		private readonly IDBContext _dbContext;
		private readonly IIndustryOrganizationHelper _helper;
		public IndustryOrganizationService(IDBContext dBContext, IIndustryOrganizationHelper helper)
		{
			_dbContext = dBContext;
			_helper = helper;
		}

		public async Task<Result> CreateAsync(IndustryOrganizationRequestDTO dto)
		{
			var conv = await _helper.Convert(dto);

			if(!conv.orgExists)
			{
				return OrganizationErrors.NotFound;
			}
			if(!conv.industryExists)
			{
				return IndustryErrors.NotFound;
			}

			bool res = await _dbContext.IndustryOrganization.CreateAsync(conv.IndustryOrganization);

			if (res)
			{
				return Result.Success();
			}

			return IndustryOrganizationErrors.CreationFailure;
		}

		public async Task<Result> DeleteAsync(IndustryOrganizationRequestDTO dto)
		{
			var conv = await _helper.Convert(dto);

			if (!conv.orgExists)
			{
				return OrganizationErrors.NotFound;
			}
			if (!conv.industryExists)
			{
				return IndustryErrors.NotFound;
			}

			var res = await _dbContext.IndustryOrganization.DeleteAsync(conv.IndustryOrganization);

			if (res)
			{
				return Result.Success();
			}

			return IndustryOrganizationErrors.DeleteUnsuccessful;
		}
	}
}
