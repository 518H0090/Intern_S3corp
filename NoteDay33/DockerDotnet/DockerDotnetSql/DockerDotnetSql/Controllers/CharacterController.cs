using DockerDotnetSql.Models;
using DockerDotnetSql.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DockerDotnetSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterRepository _repository;

        public CharacterController(ICharacterRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetAllCharacter")]
        public IActionResult GetAllCharacter()
        {
            return Ok(_repository.GetAllCharacter());
        }

        [HttpPost]
        [Route("CreateNewCharacter")]
        public IActionResult CreateNewCharacter(CreateCharacter request)
        {
            var createdCharacter = _repository.CreateNewCharacter(request);

            if (createdCharacter == null)
            {
                return BadRequest("Can't Create");
            }

            return Created("CreateValue",createdCharacter);
        }
    }
}
