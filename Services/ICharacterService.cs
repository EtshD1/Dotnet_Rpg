using Dotnet_Rpg.Models;

namespace Dotnet_Rpg.Services.CharacterService
{
	public interface ICharacterService
	{
	    Task<ResponseService<List<Character>>> GetAllCharacters();
	    Task<ResponseService<Character>> GetCharacterById(int id);
	    Task<ResponseService<List<Character>>> AddCharacter(Character newCharacter);
	}
}
