using DataManagement.Application.Abstractions;
using DataManagement.Domain.DTOs;

namespace DataManagement.Application.Services.FileServices.Data
{
	internal interface INormalizer
	{
		Task<ListWrapper> Normalize(List<FileData> data);
	}
}