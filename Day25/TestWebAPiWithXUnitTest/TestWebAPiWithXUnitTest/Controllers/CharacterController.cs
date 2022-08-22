using Microsoft.AspNetCore.Mvc;
using TestWebAPiWithXUnitTest.Models.DatabaseHelpers.Request;
using TestWebAPiWithXUnitTest.Models.DatabaseHelpers.Response;
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

        /// <summary>Gets all character.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet]
        [Route("GetAllCharacter")]
        public async Task<IActionResult> GetAllCharacter()
        {
            try
            {
                var getAllCharacter = await _characterServices.InvestigateAllCharacter();
                return Ok(getAllCharacter);
            }

            catch (Exception e)
            {
                return BadRequest(new
                {
                    message = "Can't Investigate for Character in Database",
                    Error = e.Message
                });
            }


        }

        /// <summary>Creates the character.</summary>
        /// <param name="request">The request.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpPost]
        [Route("CreateCharacter")]
        public async Task<IActionResult> CreateCharacter([FromBody] CreateCharacterRequest request)
        {

            try
            {
                var newCharacter = await _characterServices.NewCharacter(request);

                return Created("New character", newCharacter);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    message = "Can't Create New Character",
                    Error = e.Message
                });
            }


        }
    }
}