using Microsoft.AspNetCore.Mvc;
using TestWebAPiWithXUnitTest.Models.DatabaseHelpers.Request;
using TestWebAPiWithXUnitTest.Models.ServiceHelpers;

namespace TestWebAPiWithXUnitTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterServices _characterServices;

        public CharacterController(ICharacterServices characterServices)
        {
            _characterServices = characterServices;
        }

        [HttpGet]
        [Route("GetAllCharacter")]
        public async Task<IActionResult> GetAllCharacter()
        {
            var getAllCharacter = await _characterServices.InvestigateAllCharacter();

            if (getAllCharacter == null)
            {
                return BadRequest(new
                {
                    message = "Can't Investigate for Character in Database"
                });
            }

            return Ok(getAllCharacter);
        }

        [HttpPost]
        [Route("CreateCharacter")]
        public async Task<IActionResult> CreateCharacter([FromBody] CreateCharacterRequest request)
        {
            var newCharacter = await _characterServices.NewCharacter(request);

            if (newCharacter == null)
            {
                return BadRequest(new
                {
                    message = "Can't Create New Character"
                });
            }

            return Created("New character", newCharacter);
        }
    }
}