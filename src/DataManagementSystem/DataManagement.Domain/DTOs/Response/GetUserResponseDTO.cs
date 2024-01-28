namespace DataManagement.Domain.DTOs.Response
{
	public record GetUserResponseDTO(string UserName, string FirstName, string LastName, string Role, DateTime CreatedAt);
}
