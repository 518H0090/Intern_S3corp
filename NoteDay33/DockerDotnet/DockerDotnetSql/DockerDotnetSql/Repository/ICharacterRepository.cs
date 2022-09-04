using DockerDotnetSql.Models;

namespace DockerDotnetSql.Repository
{
    public interface ICharacterRepository
    {
        List<GetCharacter> GetAllCharacter();
        GetCharacter CreateNewCharacter(CreateCharacter character);
    }
}
