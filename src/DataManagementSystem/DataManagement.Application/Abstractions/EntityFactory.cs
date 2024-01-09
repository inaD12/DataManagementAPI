using DataManagement.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement.Application.Abstractions
{
	public class EntityFactory
	{
		public Country CreateCountry(string countryName)
		{
			return new Country();
		}
	}
}
