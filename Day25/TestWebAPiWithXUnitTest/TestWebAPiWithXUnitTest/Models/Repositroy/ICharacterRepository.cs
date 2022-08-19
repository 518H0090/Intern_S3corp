using TestWebAPiWithXUnitTest.Models.DatabaseHelpers.Request;
using TestWebAPiWithXUnitTest.Models.DatabaseHelpers.Response;

namespace TestWebAPiWithXUnitTest.Models.Repositroy
{
    public interface ICharacterRepository
    {
        Task<CreateCharacterResponse> CreateCharacter(CreateCharacterRequest request);

        Task<List<InvestigateCharacterResponse>> GetAllCharacter();
    }
}