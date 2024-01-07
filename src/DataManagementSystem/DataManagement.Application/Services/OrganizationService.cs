using DataManagement.Application.Abstractions;
using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs;
using DataManagement.Domain.DTOs.Request;
using DataManagement.Domain.DTOs.Response;
using DataManagement.Domain.Entities;
using DataManagement.Domain.Errors;

namespace DataManagement.Application.Services
{
	public class OrganizationService : IOrganizationService
	{
		private readonly IOrganizationRepository repository;
		private readonly ICountryRepository countryRepository;

		public OrganizationService(IRepositoryFactory repositoryFactory, ICountryRepository countryRepository)
		{
			repository = repositoryFactory.CreateOrganizationRepository();
			this.countryRepository = countryRepository;
		}

		public async Task<ResponseDTO> GetOrganizationByNameAsync(string OrganizationName)
		{
			Result result = Result.Success();

			Organization? res = await repository.GetByNameAsync(OrganizationName);

			if (res is null)
			{
				result = OrganizationErrors.NotFound;

				return new ResponseDTO(result);
			}

			Country? country = await countryRepository.GetByIdAsync(res.CountryId);

			if (country is null)
			{
				result = CountryErrors.NotFound;

				return new ResponseDTO(result);
			}

			IEnumerable <Industry> industries = await repository.GetIndustriesByOrganizationIdAsync(res.Id);

			IEnumerable<string> industryNames = industries.Select(industry => industry.Name);

			GetOrganizationResponseDTO dto = new GetOrganizationResponseDTO
				(res.Id, 
				res.OrganizationId, 
				res.Name, 
				res.Website, 
				country.Name, 
				res.Description, 
				res.Founded, 
				res.NumberOfEmployees,
				industryNames,
				(DateTime)res.CreatedAt);

			return new ResponseDTO(result, dto);
		}

		public async Task<Result> CreateOrganizationAsync(CreateOrganizationRequestDTO dto)
		{
			Country? country = await countryRepository.GetByNameAsync(dto.CountryName);

			if (country is null)
			{
				return CountryErrors.NotFound;
			}

			Organization organization = new Organization()
			{
				OrganizationId = dto.OrganizationId,
				Name = dto.Name,
				Website = dto.Website,
				CountryId = country.Id,
				Description = dto.Description,
				Founded = dto.Founded,
				NumberOfEmployees = dto.NumberOfEmployees,
			};
			organization.Set();

			bool res = await repository.CreateAsync(organization);

			if (res)
			{
				return Result.Success();
			}


			return OrganizationErrors.CreationFailure;
		}

		public async Task<Result> DeleteOrganizationAsync(string OrganizationName)
		{
			var res = await repository.SoftDeleteByNameAsync(OrganizationName);

			if (res)
			{
				return Result.Success();
			}

			return OrganizationErrors.DeleteUnsuccessful;
		}

		public async Task<Result> UpdateOrganizationAsync(UpdateOrganizationRequestDTO dto, string organizationName)
		{
			Organization? organization = await repository.GetByNameAsync(organizationName);

			if (organization is null)
			{
				return OrganizationErrors.NotFound;
			}

			Country? country = await countryRepository.GetByNameAsync(dto.CountryName);

			if (country is null)
			{
				return CountryErrors.NotFound;
			}

			organization.OrganizationId = dto.OrganizationId ?? organization.OrganizationId;
			organization.Name = dto.Name ?? organization.Name;
			organization.Website = dto.Website ?? organization.Website;
			organization.CountryId = country.Id ?? organization.CountryId;
			organization.Description = dto.Description ?? organization.Description;
			organization.Founded = dto.Founded;
			organization.NumberOfEmployees = dto.NumberOfEmployees;

			var res = await repository.UpdateAsync(organization);

			if (res)
			{
				return Result.Success();
			}

			return OrganizationErrors.NotUpdated;
		}
	}
}
