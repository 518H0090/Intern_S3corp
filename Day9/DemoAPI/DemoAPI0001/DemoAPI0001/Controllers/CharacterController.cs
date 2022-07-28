using DemoAPI0001.Models.Class;
using DemoAPI0001.Models.DataContextDB;
using DemoAPI0001.Models.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI0001.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterRespo _characterRespo;
        private readonly DataContext _context;

        public CharacterController(ICharacterRespo characterRespo,DataContext context)
        {
            _characterRespo = characterRespo;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Character>>> GetAllCharacter()
        {
            return Ok(await _characterRespo.GetAllCharacter());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Character>> GetCharacterById(int id)
        {
            return Ok(await _characterRespo.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<Character>> CreateCharacter(Character character)
        {
            if (character == null)
            {
                return BadRequest();
            }

            var characterCreated = await _characterRespo.CreateCharacter(character);
            return CreatedAtAction("CreateCharacter", new { id = characterCreated.Id }, characterCreated);
        }

        [HttpPut]
        public async Task<ActionResult<Character>> EditCharacter(Character character)
        {
            var characterEdit = await _characterRespo.EditCharacter(character);

            if (characterEdit == null)
            {
                return BadRequest();
            }

            return Ok(characterEdit);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<List<Character>>> DeleteCharacter(int id)
        {
            await _characterRespo.DeleteCharacter(id);

            return await GetAllCharacter();
        }
    }
}
