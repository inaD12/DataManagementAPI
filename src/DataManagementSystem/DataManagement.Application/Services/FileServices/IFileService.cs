using DataManagement.Domain.DTOs;

namespace DataManagement.Application.Services.FileServices
{
	public interface IFileService
	{
		Task SaveData(List<FileData> data);
	}
}