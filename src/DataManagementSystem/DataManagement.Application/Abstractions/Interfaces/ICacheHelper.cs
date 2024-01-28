namespace DataManagement.Application.Abstractions.Interfaces
{
    public interface ICacheHelper
    {
        T? Get<T>(string key);
        void Remove(string key);
        void Set<T>(string key, T data, int AbsoluteExpInM, int SlidingExpInM);
    }
}