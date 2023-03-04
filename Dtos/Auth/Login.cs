namespace Dotnet_Rpg.Dtos.Auth
{
    public class LoginDto
    {
        public string accessToken { get; set; } = string.Empty;
        public string refreshToken { get; set; } = string.Empty;
    }
}
