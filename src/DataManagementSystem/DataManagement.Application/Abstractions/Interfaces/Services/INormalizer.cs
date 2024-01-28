using DataManagement.Application.Abstractions;
using DataManagement.Domain.DTOs;

namespace DataManagement.Application.Abstractions.Interfaces.Services
{
    internal interface INormalizer
    {
        Task<ListWrapper> Normalize(List<FileData> data);
    }
}