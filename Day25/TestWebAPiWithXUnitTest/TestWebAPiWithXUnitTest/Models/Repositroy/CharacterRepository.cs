using Microsoft.EntityFrameworkCore;
using TestWebAPiWithXUnitTest.Models.DatabaseContext;
using TestWebAPiWithXUnitTest.Models.DatabaseHelpers.Request;
using TestWebAPiWithXUnitTest.Models.DatabaseHelpers.Response;
using TestWebAPiWithXUnitTest.Models.DatabaseModels;

namespace TestWebAPiWithXUnitTest.Models.Repositroy
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly DataContext _context;

        public CharacterRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>Creates the character.</summary>
        /// <param name="request">The request.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public async Task<CreateCharacterResponse> CreateCharacter(CreateCharacterRequest request)
        {
            if (request == null)
            {
                return null;
            }

            Character newCharacter = new Character
            {
                CharacterName = request.CharacterName,
                CharacterJob = request.CharacterJob
            };

            var createdCharacter = await _context.Characters.AddAsync(newCharacter);
            await _context.SaveChangesAsync();

            if (createdCharacter == null)
            {
                return null;
            }

            return new CreateCharacterResponse
            {
                CharacterName = createdCharacter.Entity.CharacterName,
                CharacterJob = createdCharacter.Entity.CharacterJob,
                CharacterLevel = createdCharacter.Entity.CharacterLevel
            };
        }

        /// <summary>Gets all character.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public async Task<List<InvestigateCharacterResponse>> GetAllCharacter()
        {
            List<InvestigateCharacterResponse> listCharacter = new List<InvestigateCharacterResponse>();

            var investigateAllCharacter = await _context.Characters.ToListAsync();

            if (investigateAllCharacter == null)
            {
                return null;
            }

            investigateAllCharacter.ForEach(invesCharacter =>
            {
                InvestigateCharacterResponse investigate = new InvestigateCharacterResponse
                {
                    CharacterName = invesCharacter.CharacterName,
                    CharacterJob = invesCharacter.CharacterJob,
                    CharacterLevel = invesCharacter.CharacterLevel
                };

                listCharacter.Add(investigate);
            });

            return listCharacter;
        }
    }
}