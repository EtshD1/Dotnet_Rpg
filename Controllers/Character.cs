using Dotnet_Rpg.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_Rpg.Controllers
{
    [ApiController]
    [Route("api/characters")]
    public class CharacterController : ControllerBase
    {
        private static List<Character> characters = new List<Character>
        {
            new Character
            {
                Id = 1,
                Name = "Nate",
                Class = RpgClass.Mage,
                Defense = 8,
                Strength = 8,
                HitPoints = 110,
                Intelligence = 20
            },
            new Character { Id = 2, Name = "Jack" }
        };

        [HttpGet]
        public ActionResult<List<Character>> GetAllCharacters()
        {
            return Ok(characters);
        }

        [HttpGet("{id}")]
        public ActionResult<Character> GetSingleCharacter(int id)
        {
            return Ok(characters.FirstOrDefault(c => c.Id == id));
        }

        [HttpPost]
        public ActionResult<Character> AddCharacter(Character character)
        {
			characters.Add(character);
			return Ok(characters);
        }
    }
}
