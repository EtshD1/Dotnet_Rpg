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
        public async Task<ActionResult<ResponseService<List<Character>>>> GetAllCharacters()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task <ActionResult<ResponseService<Character>>> GetSingleCharacter(int id)
        {
			var data = await _characterService.GetCharacterById(id);
            return data.Success ? Ok(data) : NotFound(data);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseService<List<Character>>>> AddCharacter(Character character)
        {
			return Ok(await _characterService.AddCharacter(character));
        }
    }
}
