using DataManagement.Application.Settings.Options;
using JWT.Algorithms;
using JWT.Builder;

namespace DataManagement.Application.Auth
{
	public class TokenManager : ITokenManager
	{
		private readonly JwtOptionsConfiguration _jwtOptions;

		public TokenManager(JwtOptionsConfiguration jwtOptions)
		{
			_jwtOptions = jwtOptions;
		}

		public string CreateToken(string username, string email, int secondsValid)
		{
			string token = JwtBuilder.Create()
				.WithAlgorithm(new HMACSHA256Algorithm())
				.WithSecret(_jwtOptions.SigningKey)
				.AddClaim("exp", DateTimeOffset.UtcNow.AddSeconds(secondsValid).ToUnixTimeSeconds())
				.AddClaim("iss", _jwtOptions.Issuer)
				.AddClaim("aud", _jwtOptions.Audience)
				.Encode();

			return token;
		}
	}
}
