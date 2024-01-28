namespace DataManagement.Application.Abstractions.Interfaces
{
    public interface IAccountInitializer
    {
        Task TryCreate();
    }
}