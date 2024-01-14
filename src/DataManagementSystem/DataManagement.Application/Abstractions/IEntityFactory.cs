using DataManagement.Domain.DTOs;
using DataManagement.Domain.Entities;

namespace DataManagement.Application.Abstractions
{
	public interface IEntityFactory
	{
		Country CreateCountry(string countryName);
		Industry CreateIndustry(string industryName);
		IndustryOrganization CreateIndustryOrg(string orgId, string indId);
		Organization CreateOrganization(string orgId, string name, string website, string countryId, string description, int founded, int numOfEmployees);
		User CreateUser(string? Name, string PasswordHash, string Salt, string? FirstName, string? LastName, string UserRoleId);
		UserRole CreateUserRole(string Name);
	}
}