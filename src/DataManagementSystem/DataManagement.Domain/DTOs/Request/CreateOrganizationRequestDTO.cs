namespace DataManagement.Domain.DTOs.Request
{
	public record CreateOrganizationRequestDTO(string OrganizationId, string Name, string Website, string CountryName, string Description, int Founded, int NumberOfEmployees);
}
