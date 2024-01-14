using DataManagement.Application.Abstractions;
using DataManagement.Application.Abstractions.Interfaces;
using DataManagement.Domain.DTOs;

namespace DataManagement.Application.Services.FileServices.Data
{
	internal class Normalizer : INormalizer
	{
		private readonly IDBContext _context;
		private readonly IEntityFactory _entityFactory;

		private Dictionary<string, string> DBCountries;
		private Dictionary<string, string> DBOrganizations;
		private Dictionary<string, string> DBIndustries;

		public Normalizer(IDBContext dBContext, IEntityFactory entityFactory)
		{
			_context = dBContext;
			_entityFactory = entityFactory;
		}

		public async Task<ListWrapper> Normalize(List<FileData> data)
		{
			await GetNamesAndIds();
			ListWrapper listWrapper = new ListWrapper();

			foreach (FileData file in data)
			{
				NormalizeOrganization(file, listWrapper);
			}

			DBCountries.Clear();
			DBOrganizations.Clear();
			DBIndustries.Clear();

			return listWrapper;
		}

		private async Task GetNamesAndIds()
		{
			DBCountries = await _context.Country.GetAllNamesAndIdsAsync();
			DBOrganizations = await _context.Organization.GetAllNamesAndIdsAsync();
			DBIndustries = await _context.Industry.GetAllNamesAndIdsAsync();

			//DBCountries = new Dictionary<string, string>();
			//DBOrganizations = new Dictionary<string, string>();
			//DBIndustries = new Dictionary<string, string>();
		}

		private List<string> SplitIndustries(string Industry)
		{
			List<string> splitIndustries = Industry.Split(" / ").ToList();

			return splitIndustries;
		}

		private void NormalizeOrganization(FileData data, ListWrapper listWrapper)
		{
			if (DBOrganizations.ContainsKey(data.Name))
			{
				return;
			}

			string countryId = NormalizeCountry(data.Country, listWrapper);
			List<string> industryIds = new List<string>();

			List<string> splitIndustries = SplitIndustries(data.Industry);
			foreach (string industry in splitIndustries)
			{
				string industryId = NormalizeIndustry(industry, listWrapper);

				industryIds.Add(industryId);
			}

			var organization = _entityFactory.CreateOrganization(
				data.OrganizationId,
				data.Name,
				data.Website,
				countryId,
				data.Description,
				data.Founded,
				data.NumberOfEmployees);

			organization.Set();

			listWrapper.Organizations.Add(organization);
			DBOrganizations.Add(organization.Name, organization.Id);

			foreach (string industryId in industryIds)
			{
				var IndOrg = _entityFactory.CreateIndustryOrg(organization.Id, industryId);
				listWrapper.IndOrgs.Add(IndOrg);
			}
		}

		private string NormalizeCountry(string countryName, ListWrapper listWrapper)
		{
			if (DBCountries.ContainsKey(countryName))
			{
				return DBCountries[countryName];
			}

			var country = _entityFactory.CreateCountry(countryName);
			country.Set();

			listWrapper.Countries.Add(country);
			DBCountries.Add(countryName, country.Id);

			return country.Id;
		}

		private string NormalizeIndustry(string industryName, ListWrapper listWrapper)
		{
			if (DBIndustries.ContainsKey(industryName))
			{
				return DBIndustries[industryName];
			}

			var industry = _entityFactory.CreateIndustry(industryName);
			industry.Set();

			listWrapper.Industries.Add(industry);
			DBIndustries.Add(industryName, industry.Id);

			return industry.Id;
		}
	}
}
