using DataManagement.Domain.Abstractions.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement.Domain.DTOs.Response
{
	public class LoginResponseDTO
	{
		public LoginResponseDTO()
		{
			Token = String.Empty;
		}

		public string Token { get; set; }
	}
}
