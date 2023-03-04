using AutoMapper;
using Dotnet_Rpg.Data;
using Dotnet_Rpg.Dtos.Character;
using Dotnet_Rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_Rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ResponseService<GetCharacterDto>> AddCharacter(
            AddCharacterDto newCharacter
        )
        {
            var character = _mapper.Map<Character>(newCharacter);

			_context.Characters.Add(character);
			await _context.SaveChangesAsync();

            return new ResponseService<GetCharacterDto>
            {
                Data = _mapper.Map<GetCharacterDto>(character),
                Message = $"Character with id:{character.Id} is now added"
            };
        }

        public async Task<ResponseService<List<GetCharacterDto>>> GetAllCharacters()
        {
			var characters = await _context.Characters
				.Select(c => _mapper.Map<GetCharacterDto>(c))
				.ToListAsync();

            var res = new ResponseService<List<GetCharacterDto>>
            {
                Data = characters,
                Message = "List of characters"
            };
            return res;
        }

        public async Task<ResponseService<GetCharacterDto>> GetCharacterById(int id)
        {
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
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

        public async Task<ResponseService<GetCharacterDto>> UpdateCharacter(
            int id,
            UpdateCharacterDto updatedCharacter
        )
        {
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            var res = new ResponseService<GetCharacterDto> { };
            if (character is null)
            {
                res.Message = "Character is not found";
                res.Success = false;
                return res;
            }

            character.Name = updatedCharacter.Name;
            character.Class = updatedCharacter.Class;
            character.Defense = updatedCharacter.Defense;
            character.HitPoints = updatedCharacter.HitPoints;
            character.Intelligence = updatedCharacter.Intelligence;
            character.Strength = updatedCharacter.Strength;

            res.Data = _mapper.Map<GetCharacterDto>(character);
            res.Message = "Data for single character";

            return res;
        }

        public async Task<ResponseService<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            var res = new ResponseService<List<GetCharacterDto>> { };
            if (character is null)
            {
                res.Message = "Character is not found";
                res.Success = false;
                return res;
            }

            _context.Characters.Remove(character);
			await _context.SaveChangesAsync();

            res.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            res.Message = $"List of characters after deletion character with id {id}";

			return res;
        }
    }
}
