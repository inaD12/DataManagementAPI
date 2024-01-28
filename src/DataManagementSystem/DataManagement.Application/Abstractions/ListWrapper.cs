using DataManagement.Domain.DTOs;
using DataManagement.Domain.Entities;

namespace DataManagement.Application.Abstractions
{
	public class ListWrapper
	{
        public ListWrapper()
        {
			Organizations = new List<Organization>();
			Industries = new List<Industry>();
			Countries = new List<Country>();
			IndOrgs = new List<IndustryOrganization>();
        }

        public List<Organization> Organizations { get; set; }
		public List<Industry> Industries { get; set; }
		public List<Country> Countries { get; set; }
		public List<IndustryOrganization> IndOrgs { get; set; }
	}
}
