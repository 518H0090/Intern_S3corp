using DockerDotnetSql.Models;

namespace DockerDotnetSql.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly DataContext _context;

        public CharacterRepository(DataContext context)
        {
            _context = context;
        }

        public GetCharacter CreateNewCharacter(CreateCharacter character)
        {
            Character newCharacter = new Character
            {
                CharacterName = character.CharacterName,
                CharacterJob = character.CharacterJob,
            };

            var valueCreated = _context.Characters.Add(newCharacter);
            _context.SaveChanges();

            return new GetCharacter
            {
                CharacterName = valueCreated.Entity.CharacterName,
                CharacterJob = valueCreated.Entity.CharacterJob,
                JobLevel = valueCreated.Entity.JobLevel,
            };
        }

        public List<GetCharacter> GetAllCharacter()
        {
            List<GetCharacter> result = new List<GetCharacter>();

            _context.Characters.ToList().ForEach(op =>
            {
                result.Add(new GetCharacter { CharacterName = op.CharacterName, CharacterJob = op.CharacterJob, JobLevel = op.JobLevel});
            });

            if (result.Count > 0)
            {
                return result;
            }

            return null;
        }
    }
}
