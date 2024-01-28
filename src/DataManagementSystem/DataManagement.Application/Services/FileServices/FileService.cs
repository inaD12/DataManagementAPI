using DataManagement.Application.Abstractions;
using DataManagement.Application.Abstractions.Interfaces.Services;
using DataManagement.Domain.DTOs;

namespace DataManagement.Application.Services.FileServices
{
    internal class FileService : IFileService
	{
		private readonly INormalizer _normalizer;
		private readonly IInserter _inserter;
        public FileService(INormalizer normalizer, IInserter inserter)
        {
            _normalizer = normalizer;
			_inserter = inserter;
        }
        public async Task SaveData(List<FileData> data)
		{
			ListWrapper listWrapper = await _normalizer.Normalize(data);

			_inserter.InsertData(listWrapper);
		}
	}
}
