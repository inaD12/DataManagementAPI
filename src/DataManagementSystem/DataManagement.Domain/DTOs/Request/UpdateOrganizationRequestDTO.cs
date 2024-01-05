namespace DataManagement.Domain.DTOs.Request
{
	public record UpdateOrganizationRequestDTO(string OrganizationId, string Name, string Website, string CountryId, string Description, int Founded, int NumberOfEmployees);
}
