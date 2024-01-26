using DataManagement.Application.Abstractions.Interfaces;
using DataManagement.Domain.Abstractions.Result;
using DataManagement.Domain.DTOs;
using DataManagement.Domain.DTOs.Response;
using DataManagement.Domain.Errors;

namespace DataManagement.Application.PDF
{
	internal class PDFDataRetriever : IPDFDataRetriever
	{
		private readonly IDBContext _dBContext;
		public PDFDataRetriever(IDBContext dBContext)
		{
			_dBContext = dBContext;
		}

		public async Task<ResponseDTO> Retrieve(string OrganizationName)
		{
			Result result = Result.Success();

			FileData? data = await _dBContext.FullData.GetOrganizationDataByNameAsync(OrganizationName);

			if (data is null)
			{
				result = OrganizationErrors.NotFound;

				return new ResponseDTO(result);
			}

			return new ResponseDTO(result, data);
		}
	}
}
