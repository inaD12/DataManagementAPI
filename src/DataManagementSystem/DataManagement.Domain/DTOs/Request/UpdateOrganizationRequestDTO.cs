namespace DataManagement.Domain.DTOs.Request
{
	public record UpdateOrganizationRequestDTO(string OrganizationId, string Name, string Website, string CountryName, string Description, int Founded, int NumberOfEmployees);
}
