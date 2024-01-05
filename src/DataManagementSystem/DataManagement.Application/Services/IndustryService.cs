using DataManagement.Application.Abstractions;
using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs.Request;
using DataManagement.Domain.DTOs.Response;
using DataManagement.Domain.Entities;
using DataManagement.Domain.Errors;

namespace DataManagement.Application.Services
{
	public class IndustryService : IIndustryService
	{
		private readonly IIndustryRepository repository;

		public IndustryService(IRepositoryFactory repositoryFactory)
		{
			repository = repositoryFactory.CreateIndustryRepository();
		}

		public async Task<ResponseDTO> GetIndustryByNameAsync(string industryName)
		{
			Result result = Result.Success();

			Industry? res = await repository.GetByNameAsync(industryName);

			if (res is null)
			{
				result = IndustryErrors.NotFound;

				return new ResponseDTO(result);
			}

			GetIndustryResponseDTO dto = new GetIndustryResponseDTO(res.Id, res.Name, (DateTime)res.CreatedAt);

			return new ResponseDTO(result, dto);
		}

		public async Task<Result> CreateIndustryAsync(CreateIndustryRequestDTO dto)
		{
			Industry industry = new Industry()
			{
				Name = dto.Name
			};
			industry.Set();

			bool res = await repository.CreateAsync(industry);

			if (res)
			{
				return Result.Success();
			}


			return IndustryErrors.CreationFailure;
		}

		public async Task<Result> DeleteIndustryAsync(string industryName)
		{
			var res = await repository.SoftDeleteByNameAsync(industryName);

			if (res)
			{
				return Result.Success();
			}

			return IndustryErrors.DeleteUnsuccessful;
		}

		public async Task<Result> UpdateIndustryAsync(UpdateIndustryRequestDTO dto, string industryName)
		{
			Industry? industry = await repository.GetByNameAsync(industryName);

			if (industry is null)
			{
				return IndustryErrors.NotFound;
			}

			industry.Name = dto.Name ?? industry.Name;

			var res = await repository.UpdateAsync(industry);

			if (res)
			{
				return Result.Success();
			}

			return IndustryErrors.NotUpdated;
		}
	}
}
