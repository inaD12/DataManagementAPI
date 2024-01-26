using DataManagement.Domain.DTOs;

namespace DataManagement.Application.Abstractions.Interfaces
{
	public interface IFullDataRepository
	{
		Task<FileData?> GetOrganizationDataByNameAsync(string organizationName);
	}
}