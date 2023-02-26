using Dotnet_Rpg.Models;

namespace Dotnet_Rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
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

        public async Task<ResponseService<List<Character>>> AddCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            return new ResponseService<List<Character>>
            {
                Data = characters,
                Message = $"Character with id:{newCharacter.Id} is now added"
            };
        }

        public async Task<ResponseService<List<Character>>> GetAllCharacters()
        {
            return new ResponseService<List<Character>>
            {
                Data = characters,
                Message = "List of characters"
            };
        }

        public async Task<ResponseService<Character>> GetCharacterById(int id)
        {
            var character = characters.FirstOrDefault(c => c.Id == id);
            if (character is null)
                throw new Exception("Character is not found");
            return new ResponseService<Character>{
				Data = character,
				Message = "Data for single character"
			};
        }
    }
}
