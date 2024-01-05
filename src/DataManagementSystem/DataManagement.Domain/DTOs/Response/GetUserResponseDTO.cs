using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement.Domain.DTOs.Response
{
	public record GetUserResponseDTO(string UserName, string FirstName, string LastName, DateTime CreatedAt);
}
