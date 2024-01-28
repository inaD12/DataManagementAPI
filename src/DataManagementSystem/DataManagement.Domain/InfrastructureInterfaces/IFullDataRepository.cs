using DataManagement.Domain.DTOs;

namespace DataManagement.Domain.InfrastructureInterfaces
{
	public interface IFullDataRepository
	{
		Task<FileData?> GetOrganizationDataByNameAsync(string organizationName);
	}
}