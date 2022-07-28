using DemoAPI0001.Models.Class;

namespace DemoAPI0001.Models.Interface
{
    public interface ICharacterRespo
    {
        Task<List<Character>> GetAllCharacter();
        Task<Character> GetCharacterById(int id);
        Task<Character> EditCharacter(Character character);
        Task DeleteCharacter(int id);
        Task<Character> CreateCharacter(Character character);
    }
}
