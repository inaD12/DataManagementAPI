using DataManagement.Application.Abstractions;
using DataManagement.Domain.DTOs.Request;
using DataManagement.Domain.DTOs;
using DataManagement.Domain.Entities;

namespace DataManagement.Application.Services.IndustryOrganizationServices
{
	internal class IndustryOrganizationHelper : IIndustryOrganizationHelper
	{
		private readonly IIndustryRepository _industryRepository;
		private readonly IOrganizationRepository _organizationRepository;
		public IndustryOrganizationHelper(IRepositoryFactory repositoryFactory)
		{
			_industryRepository = repositoryFactory.CreateIndustryRepository();
			_organizationRepository = repositoryFactory.CreateOrganizationRepository();
		}
		private async Task<Industry?> GetIndustry(string Name)
		{
			return await _industryRepository.GetByNameAsync(Name);
		}

		private async Task<Organization?> GetOrganization(string Name)
		{
			return await _organizationRepository.GetByNameAsync(Name);
		}

		public async Task<ConvertIndustryOrgRequestDTO> Convert(IndustryOrganizationRequestDTO dto)
		{
			bool industryExists = true;
			bool organizationExists = true;

			Industry? industry = await GetIndustry(dto.IndustryName);

			if (industry is null)
			{
				industryExists = false;
			}

			Organization? organization = await GetOrganization(dto.OrganizationName);

			if (organization is null)
			{
				organizationExists = false;
			}

			if (!industryExists || !organizationExists)
			{
				return new ConvertIndustryOrgRequestDTO(null, industryExists, organizationExists);
			}

			IndustryOrganization industryOrganization = new IndustryOrganization()
			{
				OrganizationId = organization.Id,
				IndustryId = industry.Id
			};

			return new ConvertIndustryOrgRequestDTO(industryOrganization, industryExists, organizationExists);
		}
	}
}
