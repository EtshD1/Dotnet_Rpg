using Dotnet_Rpg.Dtos.AuthToken;
using Dotnet_Rpg.Models;

namespace Dotnet_Rpg.Services.AuthToken
{
	public interface IAuthTokenService
	{
		Task<AuthTokens> GetTokens (User user);
	}
}
