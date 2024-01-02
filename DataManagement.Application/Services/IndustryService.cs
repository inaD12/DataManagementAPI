using DataManagement.Application.Abstractions;
using DataManagement.Domain.DTOs.Request;
using DataManagement.Domain.DTOs.Response;
using DataManagement.Domain.Entities;
using DataManagement.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DataManagement.Application.Services
{
	public class IndustryService : IIndustryService
	{
		private readonly IIndustryRepository repository;

		public IndustryService(IRepositoryFactory repositoryFactory)
		{
			repository = repositoryFactory.CreateIndustryRepository();
		}

		public async Task<IActionResult> GetIndustryByNameAsync(string industryName)
		{
			Industry? res = await repository.GetByNameAsync(industryName);

			if (res is null)
			{
				return new NotFoundObjectResult(IndustryErrors.NotFound);
			}

			GetIndustryResponseDTO dto = new GetIndustryResponseDTO(res.Id, res.Name, (DateTime)res.CreatedAt);

			return new OkObjectResult(dto);
		}

		public async Task<IActionResult> CreateIndustryAsync(CreateIndustryRequestDTO dto)
		{
			Industry industry = new Industry()
			{
				Name = dto.Name
			};
			industry.Set();

			bool res = await repository.CreateAsync(industry);

			if (res)
			{
				return new CreatedResult($"/api/Country/CreateAsync", dto);
			}


			return new BadRequestObjectResult(IndustryErrors.CreationFailure);
		}

		public async Task<IActionResult> DeleteIndustryAsync(string industryName)
		{
			var res = await repository.SoftDeleteByNameAsync(industryName);

			if (res)
			{
				return new OkResult();
			}

			return new BadRequestObjectResult(IndustryErrors.DeleteUnsuccessful);
		}

		public async Task<IActionResult> UpdateIndustryAsync(UpdateIndustryRequestDTO dto)
		{
			Industry industry = new Industry()
			{
				Id = dto.Id,
				Name = dto.Name
			};

			var res = await repository.UpdateAsync(industry);

			if (res)
			{
				return new OkResult();
			}

			return new BadRequestObjectResult(IndustryErrors.NotUpdated);
		}
	}
}
