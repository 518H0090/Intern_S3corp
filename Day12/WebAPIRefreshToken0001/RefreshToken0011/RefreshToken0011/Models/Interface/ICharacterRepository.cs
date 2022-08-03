using RefreshToken0011.Models.DatabaseContext;

namespace RefreshToken0011.Models.Interface
{
    public interface ICharacterRepository
    {
        Character CreateCharacter(Character character);
        List<Character> GetAllCharacters(int userId);
        Character GetCharacter(int userId);
        Character GetCharacterWithCharacterId(int userId, int characterId);
        Character EditCharacter(Character character);
        Character DeleteCharacter(int userId,int characterId);
        List<Character> DeleteAllCharacter(int id);
    }
}
