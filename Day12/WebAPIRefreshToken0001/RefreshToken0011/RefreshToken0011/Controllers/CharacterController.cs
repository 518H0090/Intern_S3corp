using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefreshToken0011.Models.DatabaseContext;
using RefreshToken0011.Models.Dto;
using RefreshToken0011.Models.Helpers;
using RefreshToken0011.Models.Interface;
using System.Text.Json.Serialization;

namespace RefreshToken0011.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterRepository _repository;
        private readonly JwtTokenHelper _tokenHelper;

        public CharacterController(ICharacterRepository repository, JwtTokenHelper TokenHelper)
        {
            _repository = repository;
            _tokenHelper = TokenHelper;
        }

        [HttpGet("ReturnUserId")]
        public int GetUserId()
        {
            var jwt = Request.Cookies["AccessToken"];

            if (jwt == null)
            {
                return 0;
            }

            var token = _tokenHelper.VerifyToken(jwt);

            var userId = int.Parse(token.Issuer);

            return userId;
        }


        [HttpGet("GetAllCharacter")]
        public ActionResult<List<Character>> GetAllCharacter()
        {
            var userId = GetUserId();

            if (userId == 0)
            {
                return NotFound(new
                {
                    message = "Not Found User Login"
                });
            }

            return _repository.GetAllCharacters(userId);
        }

        [HttpGet("GetCharacter")]
        public ActionResult<Character> GetCharacter(int characterId)
        {
            var userId = GetUserId();

            if (userId == 0)
            {
                return NotFound(new
                {
                    message = "Not Found User Login"
                });
            }

            var findCharacter = _repository.GetCharacterWithCharacterId(userId, characterId);

            if (findCharacter == null)
            {
                return BadRequest(new
                {
                    message = "Not Found Character with CharacterId or UserId"
                });
            }

            return Ok(findCharacter);
        }

        [AllowAnonymous]
        [HttpPost("CreateCharacter")]
        public ActionResult<Character> CreateCharacter([FromBody] CreateCharacteDto character)
        {
            var userId = GetUserId();

            if (userId == 0)
            {
                return NotFound(new
                {
                    message = "Not Found User Login"
                });
            }

            Character addCharacter = new Character
            {
                Name = character.Name,
                JobWorkId = character.JobId,
                UserId = userId
            };

            var characterCreated = _repository.CreateCharacter(addCharacter);

            return Created("CreatedCharacter", characterCreated);
        }

        [HttpPost("EditCharacter")]
        public ActionResult<Character> EditCharacter(UpdateCharacterDto character)
        {
            var userId = GetUserId();

            if (userId == 0)
            {
                return NotFound(new
                {
                    message = "Not Found User Login"
                });
            }

            Character updateCharacter = new Character
            {
                Id = character.Id,
                Name = character.Name,
                level = character.level,
                JobWorkId = character.JobId,
                UserId = userId
            };

            var updateCharacterRecord = _repository.EditCharacter(updateCharacter);

            if (updateCharacterRecord == null)
            {
                return NotFound(new
                {
                    message = "Can't Update Character"
                });
            }

            return Ok(updateCharacterRecord);
        }

        [HttpPost("DeleteCharacter/{characterId}")]
        public ActionResult<Character> DeleteCharacter(int characterId)
        {
            var userId = GetUserId();

            if (userId == 0)
            {
                return NotFound(new
                {
                    message = "Not Found User Login"
                });
            }

            var deleteCharacterRecord = _repository.DeleteCharacter(userId, characterId);

            if (deleteCharacterRecord == null)
            {
                return NotFound(new
                {
                    message = "Can't Delete Character"
                });
            }

            return Ok(new
            {
                message = "Delete Success",
                deleteCharacterRecord = deleteCharacterRecord
            });
        }

        [HttpPost("DeleteAllCharacter")]
        public ActionResult<Character> DeleteAllCharacter()
        {
            var userId = GetUserId();

            if (userId == 0)
            {
                return NotFound(new
                {
                    message = "Not Found User Login"
                });
            }

            var deleteAllCharacterRecord = _repository.DeleteAllCharacter(userId);

            if (deleteAllCharacterRecord == null)
            {
                return NotFound(new
                {
                    message = "Can't Delete Character"
                });
            }

            return Ok(new
            {
                message = "Delete Success",
                ListCharacterDelete = deleteAllCharacterRecord
            });
        }
    }
}
