using Dotnet_Rpg.Models;

namespace Dotnet_Rpg.Dtos.AuthToken
{
    public class AuthTokens
    {
        public string accessToken { get; set; } = string.Empty;
        public RefreshToken refreshToken { get; set; }

		public AuthTokens(RefreshToken refreshToken, string accessToken)
		{
		    this.accessToken = accessToken;
			this.refreshToken = refreshToken;
		}
    }
}
