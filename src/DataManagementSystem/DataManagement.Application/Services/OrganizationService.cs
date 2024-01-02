using DataManagement.Application.Abstractions;
using DataManagement.Domain.DTOs.Request;
using DataManagement.Domain.DTOs.Response;
using DataManagement.Domain.Entities;
using DataManagement.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DataManagement.Application.Services
{
	public class OrganizationService : IOrganizationService
	{
		private readonly IOrganizationRepository repository;

		public OrganizationService(IRepositoryFactory repositoryFactory)
		{
			repository = repositoryFactory.CreateOrganizationRepository();
		}

		public async Task<IActionResult> GetOrganizationByNameAsync(string OrganizationName)
		{
			Organization? res = await repository.GetByNameAsync(OrganizationName);

			if (res is null)
			{
				return new NotFoundObjectResult(OrganizationErrors.NotFound);
			}

			IEnumerable <Industry> industries = await repository.GetIndustriesByOrganizationIdAsync(res.Id);

			IEnumerable<string> industryNames = industries.Select(industry => industry.Name);

			GetOrganizationResponseDTO dto = new GetOrganizationResponseDTO
				(res.Id, 
				res.OrganizationId, 
				res.Name, 
				res.Website, 
				res.CountryId, 
				res.Description, 
				res.Founded, 
				res.NumberOfEmployees,
				industryNames,
				(DateTime)res.CreatedAt);

			return new OkObjectResult(dto);
		}

		public async Task<IActionResult> CreateOrganizationAsync(CreateOrganizationRequestDTO dto)
		{
			Organization organization = new Organization()
			{
				OrganizationId = dto.OrganizationId,
				Name = dto.Name,
				Website = dto.Website,
				CountryId = dto.CountryId,
				Description = dto.Description,
				Founded = dto.Founded,
				NumberOfEmployees = dto.NumberOfEmployees,
			};
			organization.Set();

			bool res = await repository.CreateAsync(organization);

			if (res)
			{
				return new CreatedResult($"/api/Organization/CreateAsync", dto);
			}


			return new BadRequestObjectResult(OrganizationErrors.CreationFailure);
		}

		public async Task<IActionResult> DeleteOrganizationAsync(string OrganizationName)
		{
			var res = await repository.SoftDeleteByNameAsync(OrganizationName);

			if (res)
			{
				return new OkResult();
			}

			return new BadRequestObjectResult(OrganizationErrors.DeleteUnsuccessful);
		}

		public async Task<IActionResult> UpdateOrganizationAsync(UpdateOrganizationRequestDTO dto)
		{
			Organization organization = new Organization()
			{
				Id = dto.Id,
				OrganizationId = dto.OrganizationId,
				Name = dto.Name,
				Website = dto.Website,
				CountryId = dto.CountryId,
				Description = dto.Description,
				Founded = dto.Founded,
				NumberOfEmployees = dto.NumberOfEmployees,
			};

			var res = await repository.UpdateAsync(organization);

			if (res)
			{
				return new OkResult();
			}

			return new BadRequestObjectResult(OrganizationErrors.NotUpdated);
		}
	}
}
