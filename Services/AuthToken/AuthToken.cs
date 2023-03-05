using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Dotnet_Rpg.Data;
using Dotnet_Rpg.Dtos.AuthToken;
using Dotnet_Rpg.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_Rpg.Services.AuthToken
{
    public class AuthTokenService : IAuthTokenService
    {
        private readonly IConfiguration _config;
        private readonly DataContext _context;
        private readonly SymmetricSecurityKey _key;

        public AuthTokenService(IConfiguration config, DataContext context)
        {
            _config = config;
            _context = context;
            var key = _config.GetSection("AppSettings:Token").Value;

            if (key is not null)
            {
                _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                return;
            }
            Console.Error.WriteLine("Token key is not found");
            throw new Exception("Token key is not found");
        }

        public async Task<AuthTokens> GetTokens(User user)
        {
			var accessToken = GenerateAccessToken(user);
			var refreshToken = GenerateRefreshToken(user);

            return new AuthTokens(await refreshToken, accessToken);
        }

        private string GenerateAccessToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<RefreshToken> GenerateRefreshToken(User user)
        {
			var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

			if (await _context.RefreshTokens.AnyAsync(c => c.Token == token))
				return await GenerateRefreshToken(user);

			var refreshToken = new RefreshToken
			{
				Token = token,
				Expires = DateTime.Now.AddDays(10),
				Created = DateTime.Now,
				User = user,
				UserId = user.Id
			};
			return refreshToken;
        }
    }
}
