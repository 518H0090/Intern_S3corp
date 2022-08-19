using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using TestWebAPiWithXUnitTest.Models.DatabaseContext;
using TestWebAPiWithXUnitTest.Models.DatabaseHelpers.Request;
using TestWebAPiWithXUnitTest.Models.DatabaseHelpers.Response;
using TestWebAPiWithXUnitTest.Models.DatabaseModels;
using TestWebAPiWithXUnitTest.Models.Repositroy;

namespace XUnitTestForWebAPI
{
    public class RepositoryUnitTest
    {
        private Fixture _fixture;
        private ICharacterRepository _characterRepository;

        public RepositoryUnitTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task CheckReturnsAllCharacter()
        {
            
        }
    }
}