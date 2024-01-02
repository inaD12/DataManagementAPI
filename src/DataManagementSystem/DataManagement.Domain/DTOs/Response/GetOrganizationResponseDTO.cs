namespace DataManagement.Domain.DTOs.Response
{
	public record GetOrganizationResponseDTO(
		string Id, 
		string OrganizationId, 
		string Name, 
		string Website, 
		string CountryId, 
		string Description, 
		int Founded, 
		int NumberOfEmployees,
		IEnumerable<string> Industries,
		DateTime CreatedAt);
}
