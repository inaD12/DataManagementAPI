using DataManagement.Application.Abstractions.Interfaces;
using DataManagement.Domain.DTOs;
using DataManagement.Domain.Entities;

namespace DataManagement.Application.Abstractions
{
    public class EntityFactory : IEntityFactory
	{
		public Country CreateCountry(string countryName)
		{
			return new Country()
			{
				Name = countryName
			};
		}

		public Industry CreateIndustry(string industryName)
		{
			return new Industry()
			{
				Name = industryName
			};
		}

		public IndustryOrganization CreateIndustryOrg(string orgId, string indId)
		{
			return new IndustryOrganization()
			{
				OrganizationId = orgId,
				IndustryId = indId
			};
		}

		public Organization CreateOrganization(string orgId, string name, string website, string countryId, string description, int founded, int numOfEmployees)
		{
			return new Organization()
			{
				OrganizationId = orgId,
				Name = name,
				Website = website,
				CountryId = countryId,
				Description = description,
				Founded = founded,
				NumberOfEmployees = numOfEmployees
			};
		}

		public User CreateUser(string? Name, string PasswordHash, string Salt, string? FirstName, string? LastName, string UserRoleId)
		{
			return new User()
			{
				Name = Name,
				PasswordHash = PasswordHash,
				Salt = Salt,
				FirstName = FirstName,
				LastName = LastName,
				UserRoleId = UserRoleId
			};
		}

		public UserRole CreateUserRole(string Name)
		{
			return new UserRole()
			{
				Name = Name
			};
		}
	}
}
