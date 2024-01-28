using DataManagement.Application.Abstractions.Interfaces;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.IdentityModel.Tokens.Jwt;

namespace DataManagement.Application.Helpers
{
    public class JWTParser : IJWTParser
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public JWTParser(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public string GetUsernameFromToken()
		{
			return GetClaimValueFromToken("username");
		}

		private string GetClaimValueFromToken(string claimType)
		{
			try
			{
				var jwtToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

				if (string.IsNullOrEmpty(jwtToken))
				{
					return null;
				}

				var handler = new JwtSecurityTokenHandler();
				var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;

				string claimValue = jsonToken?.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
				return claimValue;
			}
			catch (Exception ex)
			{
				Log.Error($"Error parsing token: {ex.Message}");
				return string.Empty;
			}
		}
	}
}
