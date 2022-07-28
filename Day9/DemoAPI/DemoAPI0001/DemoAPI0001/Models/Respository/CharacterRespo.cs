using DemoAPI0001.Models.Interface;
using DemoAPI0001.Models.DataContextDB;
using DemoAPI0001.Models.Class;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI0001.Models.Respository
{
    public class CharacterRespo : ICharacterRespo
    {
        private readonly DataContext _context;

        public CharacterRespo(DataContext context)
        {
            _context = context;
        }

        public async Task<Character> CreateCharacter(Character character)
        {
            if (character != null)
            {
                var characterCreate = await _context.Characters.AddAsync(character);
                await _context.SaveChangesAsync();
                return characterCreate.Entity;
            }

            return null;
        }

        public async Task DeleteCharacter(int id)
        {
            if (id != null)
            {
                var characterDelete = await _context.Characters.FindAsync(id);

                if (characterDelete != null)
                {
                    _context.Characters.Remove(characterDelete);
                    await _context.SaveChangesAsync();
                }

            }
        }

        public async Task<Character> EditCharacter(Character character)
        {
            if (character != null)
            {
                var characterEdit = await _context.Characters.FindAsync(character.Id);

                if (characterEdit != null)
                {
                    characterEdit.Name = character.Name;
                    characterEdit.Job = character.Job;

                    _context.Characters.Update(characterEdit);
                    await _context.SaveChangesAsync();

                    return characterEdit;
                }
                
            }

            return null;
        }

        public async Task<List<Character>> GetAllCharacter()
        {
            return await _context.Characters.ToListAsync();
        }

        public async Task<Character> GetCharacterById(int id)
        {
            return await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
