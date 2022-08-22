using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using TestWebAPiWithXUnitTest.Controllers;
using TestWebAPiWithXUnitTest.Models.DatabaseHelpers.Request;
using TestWebAPiWithXUnitTest.Models.DatabaseHelpers.Response;
using TestWebAPiWithXUnitTest.Models.ServiceHelpers;

namespace XUnitTestForWebAPI
{
    public class CharacterControllerUnitTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<ICharacterServices> _mockServices;

        public CharacterControllerUnitTest()
        {
            _fixture = new Fixture();
            _mockServices = new Mock<ICharacterServices>();
        }

        /// <summary>Gets all character HTTP get all value with200 ok.</summary>
        [Fact]
        public async Task GetAllCharacter_HttpGetAllValueWith200Ok()
        {
            //Arrange
            var listValue = _fixture.CreateMany<InvestigateCharacterResponse>(5).ToList();
            _mockServices.Setup(x => x.InvestigateAllCharacter()).ReturnsAsync(listValue);

            CharacterController controller = new CharacterController(_mockServices.Object);

            //Action
            var getHttpObjectValue = await controller.GetAllCharacter();
            var getHttpValue = getHttpObjectValue as ObjectResult;
            var statusCheck = getHttpValue.StatusCode;

            //Assert
            Assert.Equal(200, statusCheck);
            Assert.IsAssignableFrom<IActionResult>(getHttpObjectValue);
        }

        /// <summary>Gets all character HTTP get all value with400 bad request.</summary>
        [Fact]
        public async Task GetAllCharacter_HttpGetAllValueWith400BadRequest()
        {
            //Arrange
            _mockServices.Setup(x => x.InvestigateAllCharacter()).Throws(new Exception());

            CharacterController controller = new CharacterController(_mockServices.Object);

            //Action
            var getHttpObjectValue = await controller.GetAllCharacter();
            var getHttpValue = getHttpObjectValue as ObjectResult;
            var statusCheck = getHttpValue.StatusCode;

            //Assert
            Assert.Equal(400, statusCheck);
            Assert.IsAssignableFrom<IActionResult>(getHttpObjectValue);
        }

        /// <summary>Creates the character HTTP post for create201 created.</summary>
        [Fact]
        public async Task CreateCharacter_HttpPostForCreate201Created()
        {
            //Arrange
            var newCharacter = _fixture.Create<CreateCharacterRequest>();

            var responseNewCharacter = new CreateCharacterResponse
            {
                CharacterName = newCharacter.CharacterName,
                CharacterJob = newCharacter.CharacterJob,
            };

            _mockServices.Setup(x => x.NewCharacter(newCharacter)).ReturnsAsync(responseNewCharacter);
            CharacterController controller = new CharacterController(_mockServices.Object);

            //Action
            var getObjectValue = await controller.CreateCharacter(newCharacter);
            var getCheckHttp = getObjectValue as ObjectResult;
            var statusCheck = getCheckHttp.StatusCode;

            //Assert
            Assert.Equal(201, statusCheck);
            Assert.IsAssignableFrom<IActionResult>(getObjectValue);

        }

        /// <summary>Creates the character HTTP post for created bad request.</summary>
        [Fact]
        public async Task CreateCharacter_HttpPostForCreatedBadRequest()
        {
            //Arrange
            var newCharacter = _fixture.Create<CreateCharacterRequest>();
            _mockServices.Setup(x => x.NewCharacter(newCharacter)).Throws(new Exception());
            CharacterController controller = new CharacterController(_mockServices.Object);

            //Action
            var getObjectValue = await controller.CreateCharacter(newCharacter);
            var getActionResult = getObjectValue as ActionResult;
            var getCheckHttp = getObjectValue as ObjectResult;


            //Assert
            Assert.Equal(400, getCheckHttp.StatusCode);
            _mockServices.Verify(x => x.NewCharacter(newCharacter), Times.Once);
        }
    }
}