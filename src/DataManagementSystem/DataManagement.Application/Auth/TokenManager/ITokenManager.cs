namespace DataManagement.Application.Auth.TokenManager
{
    public interface ITokenManager
    {
        string CreateToken(string username, string role, int secondsValid);
    }
}