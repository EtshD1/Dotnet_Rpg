using AutoMapper;
using Dotnet_Rpg.Dtos.Character;
using Dotnet_Rpg.Models;

namespace Dotnet_Rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

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

        public async Task<ResponseService<List<GetCharacterDto>>> AddCharacter(
            AddCharacterDto newCharacter
        )
        {
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;

            characters.Add(character);
            return new ResponseService<List<GetCharacterDto>>
            {
                Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList(),
                Message = $"Character with id:{character.Id} is now added"
            };
        }

        public async Task<ResponseService<List<GetCharacterDto>>> GetAllCharacters()
        {
            var res = new ResponseService<List<GetCharacterDto>>
            {
                Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList(),
                Message = "List of characters"
            };
            return res;
        }

        public async Task<ResponseService<GetCharacterDto>> GetCharacterById(int id)
        {
            var character = characters.FirstOrDefault(c => c.Id == id);
            var res = new ResponseService<GetCharacterDto>
            {
                Data = _mapper.Map<GetCharacterDto>(character),
                Message = "Data for single character"
            };
            if (character is null)
            {
                res.Message = "Character is not found";
                res.Success = false;
            }
            return res;
        }
    }
}
