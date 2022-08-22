using AutoFixture;
using Moq;
using TestWebAPiWithXUnitTest.Models.DatabaseHelpers.Request;
using TestWebAPiWithXUnitTest.Models.DatabaseHelpers.Response;
using TestWebAPiWithXUnitTest.Models.Repositroy;
using TestWebAPiWithXUnitTest.Models.ServiceHelpers;

namespace XUnitTestForWebAPI
{
    public class ServiceUnitTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<ICharacterRepository> _mockRepository;

        public ServiceUnitTest()
        {
            _fixture = new Fixture();
            _mockRepository = new Mock<ICharacterRepository>();
        }

        /// <summary>
        ///   <para>
        ///     <br />
        ///   </para>
        ///   <para>Investigates all character check have value.
        /// </para>
        /// </summary>
        [Fact]
        public async Task InvestigateAllCharacter_CheckHaveValue()
        {
            //Arrange
            var listValueReturn = _fixture.CreateMany<InvestigateCharacterResponse>(5).ToList();
            _mockRepository.Setup(x => x.GetAllCharacter()).ReturnsAsync(listValueReturn);

            var characterServices = new CharacterServices(_mockRepository.Object);

            //Action
            var intestigateAllValue = await characterServices.InvestigateAllCharacter();

            //Assert
            Assert.Equal<List<InvestigateCharacterResponse>>(listValueReturn, intestigateAllValue);
        }

        /// <summary>Investigates all character check without value.</summary>
        [Fact]
        public async Task InvestigateAllCharacter_CheckWithoutValue()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetAllCharacter()).ReturnsAsync(new List<InvestigateCharacterResponse>());
            

            var characterServices = new CharacterServices(_mockRepository.Object);

            //Action
            var intestigateAllValue = await characterServices.InvestigateAllCharacter();

            //Assert
            Assert.Empty(intestigateAllValue);
            _mockRepository.Verify(x => x.GetAllCharacter(), Times.Once);
        }

        /// <summary>Creates new character_checkwithvalue.</summary>
        [Fact]
        public async Task NewCharacter_CheckWithValue()
        {
            //Arrange
            var newCharacter = _fixture.Create<CreateCharacterRequest>();

            var responseExpected = new CreateCharacterResponse
            {
                CharacterName = newCharacter.CharacterName,
                CharacterJob = newCharacter.CharacterJob,
            };

            _mockRepository.Setup(x => x.CreateCharacter(It.IsAny<CreateCharacterRequest>())).ReturnsAsync(responseExpected);

            var characterServices = new CharacterServices(_mockRepository.Object);

            //Action
            var createNewCharacter = await characterServices.NewCharacter(newCharacter);

            //Assert
            Assert.NotEmpty(createNewCharacter.ToString());
            Assert.Equal(newCharacter.CharacterName, createNewCharacter.CharacterName);

            _mockRepository.Verify(x => x.CreateCharacter(It.IsAny<CreateCharacterRequest>()), Times.Once);
        }

        /// <summary>Creates new character_checkwithoutvalue.</summary>
        [Fact]
        public async Task NewCharacter_CheckWithoutValue()
        {
            //Arrange
            var newCharacter = _fixture.Create<CreateCharacterRequest>();

            _mockRepository.Setup(x => x.CreateCharacter(It.IsAny<CreateCharacterRequest>())).ReturnsAsync(new CreateCharacterResponse());

            var characterServices = new CharacterServices(_mockRepository.Object);

            //Action
            var createNewCharacter = await characterServices.NewCharacter(newCharacter);

            //Assert
            Assert.NotEmpty(createNewCharacter.ToString());
            Assert.NotEqual(newCharacter.CharacterName, createNewCharacter.CharacterName);

            _mockRepository.Verify(x => x.CreateCharacter(It.IsAny<CreateCharacterRequest>()), Times.Once);
        }

        /// <summary>Creates new character_checkwithvalueandwrongvalueresponse.</summary>
        [Fact]
        public async Task NewCharacter_CheckWithValueAndWrongValueResponse()
        {
            //Arrange
            var newCharacter = _fixture.Create<CreateCharacterRequest>();

            var responseExpected = _fixture.Create<CreateCharacterResponse>();
            _mockRepository.Setup(x => x.CreateCharacter(It.IsAny<CreateCharacterRequest>())).ReturnsAsync(new CreateCharacterResponse());

            var characterServices = new CharacterServices(_mockRepository.Object);

            //Action
            var createNewCharacter = await characterServices.NewCharacter(newCharacter);

            //Assert
            Assert.NotEmpty(createNewCharacter.ToString());
            Assert.NotEqual(responseExpected.CharacterName, createNewCharacter.CharacterName);

            _mockRepository.Verify(x => x.CreateCharacter(It.IsAny<CreateCharacterRequest>()), Times.Once);
        }
    }
}