namespace DataManagement.Domain.DTOs.Request
{
	public record UpdateOrganizationRequestDTO(string Id, string OrganizationId, string Name, string Website, string CountryId, string Description, int Founded, int NumberOfEmployees);
}
