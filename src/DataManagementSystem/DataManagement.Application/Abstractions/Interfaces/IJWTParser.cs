namespace DataManagement.Application.Abstractions.Interfaces
{
    public interface IJWTParser
    {
        string GetUsernameFromToken();
    }
}