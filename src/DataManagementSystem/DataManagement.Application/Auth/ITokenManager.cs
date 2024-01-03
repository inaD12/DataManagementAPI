namespace DataManagement.Application.Auth
{
	public interface ITokenManager
	{
		string CreateToken(string username, string email, int secondsValid);
	}
}