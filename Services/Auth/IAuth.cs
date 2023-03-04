using Dotnet_Rpg.Dtos.Auth;
using Dotnet_Rpg.Models;

namespace Dotnet_Rpg.Services.AuthService
{
	public interface IAuthService
	{
		Task<ResponseService<LoginDto>> Register (string username, string password);
		Task<ResponseService<LoginDto>> Login (string username, string password);
	}
}
