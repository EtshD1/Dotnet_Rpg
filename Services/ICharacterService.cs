using Dotnet_Rpg.Models;

namespace Dotnet_Rpg.Services.CharacterService
{
	public interface ICharacterService
	{
	    Task<List<Character>> GetAllCharacters();
	    Task<Character> GetCharacterById(int id);
	    Task<List<Character>> AddCharacter(Character newCharacter);
	}
}
