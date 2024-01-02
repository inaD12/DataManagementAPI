using DataManagement.Application.Abstractions;
using DataManagement.Domain.Abstractions;
using DataManagement.Domain.DTOs.Request;
using DataManagement.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

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

		public async Task<IActionResult> CreateAsync(IndustryOrganizationRequestDTO dto)
		{
			var conv = await _helper.Convert(dto);

			if(!conv.orgExists)
			{
				return new NotFoundObjectResult(OrganizationErrors.NotFound);
			}
			if(!conv.industryExists)
			{
				return new NotFoundObjectResult(IndustryErrors.NotFound);
			}

			bool res = await _repository.CreateAsync(conv.IndustryOrganization);

			if (res)
			{
				return new CreatedResult($"/api/IndustryOrganization/CreateAsync", dto);
			}

			return new BadRequestObjectResult(IndustryOrganizationErrors.CreationFailure);
		}

		public async Task<IActionResult> DeleteAsync(IndustryOrganizationRequestDTO dto)
		{
			var conv = await _helper.Convert(dto);

			if (!conv.orgExists)
			{
				return new NotFoundObjectResult(OrganizationErrors.NotFound);
			}
			if (!conv.industryExists)
			{
				return new NotFoundObjectResult(IndustryErrors.NotFound);
			}

			var res = await _repository.DeleteAsync(conv.IndustryOrganization);

			if (res)
			{
				return new OkResult();
			}

			return new BadRequestObjectResult(IndustryOrganizationErrors.DeleteUnsuccessful);
		}
	}
}
