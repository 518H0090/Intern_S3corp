using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestNlogInWebAPI;

namespace TestNlogInWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ILogger<CharactersController> _logger;

        public CharactersController(DataContext context, ILogger<CharactersController> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogDebug(1, "Nlog injected into WeatherController");
        }

        // GET: api/Characters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Character>>> GetCharacters()
        {
          if (_context.Characters == null)
          {
              return NotFound();
          }
            _logger.LogInformation("Hello HuynhTranTrungHieu");
            return await _context.Characters.ToListAsync();
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacter(int id)
        {
          if (_context.Characters == null)
          {
              return NotFound();
          }
            var character = await _context.Characters.FindAsync(id);

            if (character == null)
            {
                return NotFound();
            }
            _logger.LogInformation("Hello HuynhTranTrungHieu");
            return character;
        }

        // PUT: api/Characters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, Character character)
        {
            if (id != character.Id)
            {
                return BadRequest();
            }

            _context.Entry(character).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            _logger.LogInformation("Hello HuynhTranTrungHieu");
            return NoContent();
        }

        // POST: api/Characters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(Character character)
        {
          if (_context.Characters == null)
          {
              return Problem("Entity set 'DataContext.Characters'  is null.");
          }
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Hello HuynhTranTrungHieu");
            return CreatedAtAction("GetCharacter", new { id = character.Id }, character);
        }

        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            if (_context.Characters == null)
            {
                return NotFound();
            }
            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Hello HuynhTranTrungHieu");
            return NoContent();
        }

        private bool CharacterExists(int id)
        {
            _logger.LogInformation("Hello HuynhTranTrungHieu");
            return (_context.Characters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
