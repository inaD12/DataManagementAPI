using DataManagement.Application.Abstractions;

namespace DataManagement.Application.Services.FileServices.Data
{
	internal interface IInserter
	{
		void InsertData(ListWrapper data);
	}
}