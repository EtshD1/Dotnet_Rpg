using Dotnet_Rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_Rpg.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Character> Characters => Set<Character>();

		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		    
		}
    }
}
