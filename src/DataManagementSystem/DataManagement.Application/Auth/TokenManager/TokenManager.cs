
using DataManagement.Application.Settings.Options;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.Extensions.Options;
using Serilog;

namespace DataManagement.Application.Auth.TokenManager
{
    public class TokenManager : ITokenManager
    {
        private readonly IOptionsMonitor<JwtOptionsConfiguration> _jwtOptions;

        public TokenManager(IOptionsMonitor<JwtOptionsConfiguration> jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public string CreateToken(int secondsValid)
        {
            try
            {
                string token = JwtBuilder.Create()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret(_jwtOptions.CurrentValue.SigningKey)
                    .AddClaim("exp", DateTimeOffset.UtcNow.AddSeconds(secondsValid).ToUnixTimeSeconds())
                    .AddClaim("iss", _jwtOptions.CurrentValue.Issuer)
                    .AddClaim("aud", _jwtOptions.CurrentValue.Audience)
                    .Encode();

				return token;
			}
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }

            return null;
        }
    }
}
