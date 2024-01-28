namespace DataManagement.Application.Abstractions.Interfaces
{
    public interface ITokenManager
    {
        string CreateToken(string username, string role, int secondsValid);
    }
}