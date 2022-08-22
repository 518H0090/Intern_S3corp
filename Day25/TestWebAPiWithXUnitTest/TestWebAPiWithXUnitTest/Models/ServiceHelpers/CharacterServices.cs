using TestWebAPiWithXUnitTest.Models.DatabaseHelpers.Request;
using TestWebAPiWithXUnitTest.Models.DatabaseHelpers.Response;
using TestWebAPiWithXUnitTest.Models.Repositroy;

namespace TestWebAPiWithXUnitTest.Models.ServiceHelpers
{
    public class CharacterServices : ICharacterServices
    {
        private readonly ICharacterRepository _repository;

        public CharacterServices(ICharacterRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        ///   <para>
        ///     <br />
        ///   </para>
        ///   <para>Investigates all character.
        /// </para>
        /// </summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public async Task<List<InvestigateCharacterResponse>> InvestigateAllCharacter()
        {
            var getAllCharacter = await _repository.GetAllCharacter();

            if (getAllCharacter == null)
            {
                //return null;

                throw new Exception();
            }

            return getAllCharacter;
        }

        /// <summary>
        ///   <para>
        ///     <br />
        ///   </para>
        ///   <para>Creates new character.
        /// </para>
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public async Task<CreateCharacterResponse> NewCharacter(CreateCharacterRequest request)
        {
            var createdCharacter = await _repository.CreateCharacter(request);

            if (createdCharacter == null)
            {
                return null;
            }

            return createdCharacter;
        }
    }
}