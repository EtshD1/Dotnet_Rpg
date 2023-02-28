using Dotnet_Rpg.Dtos.Character;
using Dotnet_Rpg.Models;
using Dotnet_Rpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_Rpg.Controllers
{
    [ApiController]
    [Route("api/characters")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

		public CharacterController(ICharacterService characterService)
		{
            _characterService = characterService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseService<List<GetCharacterDto>>>> GetAllCharacters()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task <ActionResult<ResponseService<GetCharacterDto>>> GetSingleCharacter(int id)
        {
			var data = await _characterService.GetCharacterById(id);
            return data.Success ? Ok(data) : NotFound(data);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseService<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto character)
        {
			return Ok(await _characterService.AddCharacter(character));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ResponseService<GetCharacterDto>>> UpdateCharacter(int id, UpdateCharacterDto updatedCharacter)
        {
            var data = await _characterService.UpdateCharacter(id, updatedCharacter);
			return data.Success ? Ok(data) : NotFound(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseService<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var data = await _characterService.DeleteCharacter(id);
			return data.Success ? Ok(data) : NotFound(data);
        }
    }
}
