using Dotnet_Rpg.Dtos.Character;
using Dotnet_Rpg.Models;

namespace Dotnet_Rpg.Services.CharacterService
{
	public interface ICharacterService
	{
	    Task<ResponseService<List<GetCharacterDto>>> GetAllCharacters();
	    Task<ResponseService<GetCharacterDto>> GetCharacterById(int id);
	    Task<ResponseService<GetCharacterDto>> AddCharacter(AddCharacterDto newCharacter);
	    Task<ResponseService<GetCharacterDto>> UpdateCharacter(int id, UpdateCharacterDto updatedCharacter);
	    Task<ResponseService<List<GetCharacterDto>>> DeleteCharacter(int id);
	}
}
