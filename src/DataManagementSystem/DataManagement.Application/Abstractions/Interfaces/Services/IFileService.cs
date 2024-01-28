using DataManagement.Domain.DTOs;

namespace DataManagement.Application.Abstractions.Interfaces.Services
{
    public interface IFileService
    {
        Task SaveData(List<FileData> data);
    }
}