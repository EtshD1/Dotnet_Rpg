using Dotnet_Rpg.Data;
using Dotnet_Rpg.Dtos.Auth;
using Dotnet_Rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_Rpg.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<ResponseService<User>> Login(AuthDto user)
        {
            var dbUser = await _context.Users
                .Where(u => u.Username == user.Username)
                .FirstOrDefaultAsync();

            var res = new ResponseService<User> { };

            if (
					dbUser is null || !VerifyPasswordHash(
						password: user.Password,
						passwordHash: dbUser.PasswordHash,
						passwordSalt: dbUser.PasswordSalt
					)
				)
            {
                res.Message = "Username/Password is incorrect.";
                res.Success = false;
                return res;
            }

            res.Data = dbUser;
            res.Message = "Access granted.";

            return res;
        }

        public async Task<ResponseService<User>> Register(AuthDto user)
        {
            var response = new ResponseService<User>();
            if (await UserExists(user.Username))
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            }

            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = new User
            {
                Username = user.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            response.Data = newUser;
            response.Message = "User registered.";

            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower());
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
