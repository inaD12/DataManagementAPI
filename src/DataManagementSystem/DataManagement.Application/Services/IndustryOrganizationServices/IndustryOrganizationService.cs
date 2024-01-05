using DataManagement.Application.Abstractions;
using DataManagement.Domain.Abstractions;
using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs.Request;
using DataManagement.Domain.Errors;

namespace DataManagement.Application.Services.IndustryOrganizationServices
{
	internal class IndustryOrganizationService : IIndustryOrganizationService
	{
		private readonly IIndustryOrganizationRepository _repository;
		private readonly IIndustryOrganizationHelper _helper;
		public IndustryOrganizationService(IRepositoryFactory repositoryFactory, IIndustryOrganizationHelper helper)
		{
			_repository = repositoryFactory.CreateIndustryOrganizationRepository();
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

			bool res = await _repository.CreateAsync(conv.IndustryOrganization);

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

			var res = await _repository.DeleteAsync(conv.IndustryOrganization);

			if (res)
			{
				return Result.Success();
			}

			return IndustryOrganizationErrors.DeleteUnsuccessful;
		}
	}
}
