using System.ComponentModel.DataAnnotations;

namespace Dotnet_Rpg.Models
{
	public class RefreshToken
	{
		[Key]
		public string Token { get; set; } = "";
		public DateTime Created { get; set; } = DateTime.Now;
		public DateTime Expires { get; set; } = DateTime.Now.AddDays(10);
		public User User { get; set; } = new User { };
		public int UserId { get; set; }
	}
}
