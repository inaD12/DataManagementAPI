namespace DataManagement.Application.Auth.TokenManager
{
    public interface ITokenManager
    {
        string CreateToken(int secondsValid);
    }
}