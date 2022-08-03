using Microsoft.EntityFrameworkCore;
using RefreshToken0011.Models.DatabaseContext;
using RefreshToken0011.Models.Interface;
using RefreshToken0011.Models.ModelDatabase;

namespace RefreshToken0011.Models.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly DataContext _context;

        public CharacterRepository(DataContext context)
        {
            _context = context;
        }

        public Character CreateCharacter(Character character)
        {
            _context.Characters.Add(character);
            character.Id = _context.SaveChanges();
            return character;
        }

        public List<Character> DeleteAllCharacter(int id)
        {
            var findCharacter = GetAllCharacters(id);

            if (findCharacter == null)
            {
                return null;
            }

            _context.Characters.RemoveRange(findCharacter);
            _context.SaveChanges();

            return findCharacter;
        }

        public Character DeleteCharacter(int userId, int characterId)
        {
            var findCharacter = GetCharacterWithCharacterId(userId, characterId);

            if (findCharacter == null)
            {
                return null;
            }

            var deleteCharacter = _context.Characters.Remove(findCharacter);
            _context.SaveChanges();

            return deleteCharacter.Entity;
        }

        public Character EditCharacter(Character character)
        {
            var findCharacter = GetCharacterWithCharacterId(character.UserId,character.Id);

            if (findCharacter == null)
            {
                return null;
            }

            findCharacter.Name = character.Name;
            findCharacter.level = character.level;
            findCharacter.JobWorkId = character.JobWorkId;

            var characterUpdate = _context.Characters.Update(findCharacter);
            _context.SaveChanges();

            return characterUpdate.Entity;
        }

        public List<Character> GetAllCharacters(int userId)
        {
            return _context.Characters.Where(c => c.UserId == userId).Include(c => c.User).Include(c => c.Job).ToList();
        }

        public Character GetCharacter(int userId)
        {
            return _context.Characters.Where(c => c.UserId == userId).Include(c => c.User).Include(c => c.Job).FirstOrDefault();
        }

        public Character GetCharacterWithCharacterId(int userId, int characterId)
        {
            return _context.Characters.Where(c => c.UserId == userId && c.Id == characterId).Include(c => c.User).Include(c => c.Job).FirstOrDefault();
        }
    }
}
