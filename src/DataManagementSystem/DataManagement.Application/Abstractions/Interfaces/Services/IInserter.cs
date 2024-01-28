using DataManagement.Application.Abstractions;

namespace DataManagement.Application.Abstractions.Interfaces.Services
{
    internal interface IInserter
    {
        void InsertData(ListWrapper data);
    }
}