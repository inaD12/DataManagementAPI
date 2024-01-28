namespace DataManagement.Application.Abstractions.Interfaces
{
    public interface IPasswordManager
    {
        string HashPassword(string password, out string salt);
        bool VerifyPassword(string password, string hash, string salt);
    }
}