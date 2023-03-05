using Dotnet_Rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_Rpg.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Character> Characters => Set<Character>();
        public DbSet<User> Users => Set<User>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }
    }
}
