using Dotnet_Rpg.Dtos.Auth;
using Dotnet_Rpg.Models;

namespace Dotnet_Rpg.Services.UserService
{
    public interface IUserService
    {
        Task<ResponseService<User>> Register(AuthDto user);
        Task<ResponseService<User>> Login(AuthDto user);
        Task<bool> UserExists(string username);
    }
}
