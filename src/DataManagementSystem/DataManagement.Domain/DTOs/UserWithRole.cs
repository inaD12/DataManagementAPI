namespace DataManagement.Domain.DTOs
{
	public record UserWithRole(string Id, string Name, string PasswordHash, string Salt, string FirstName, string LastName, string Role, DateTime? CreatedAt);
}
