using TestWebAPiWithXUnitTest.Models.DatabaseHelpers.Request;
using TestWebAPiWithXUnitTest.Models.DatabaseHelpers.Response;

namespace TestWebAPiWithXUnitTest.Models.ServiceHelpers
{
    public interface ICharacterServices
    {
        Task<List<InvestigateCharacterResponse>> InvestigateAllCharacter();

        Task<CreateCharacterResponse> NewCharacter(CreateCharacterRequest request);
    }
}