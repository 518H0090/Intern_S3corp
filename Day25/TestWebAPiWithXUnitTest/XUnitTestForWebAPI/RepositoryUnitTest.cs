using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using TestWebAPiWithXUnitTest.Models.DatabaseContext;
using TestWebAPiWithXUnitTest.Models.DatabaseHelpers.Request;
using TestWebAPiWithXUnitTest.Models.DatabaseHelpers.Response;
using TestWebAPiWithXUnitTest.Models.DatabaseModels;
using TestWebAPiWithXUnitTest.Models.Repositroy;

namespace XUnitTestForWebAPI
{
    public class RepositoryUnitTest
    {
        private Fixture _fixture;
        private static DataContext _context;

        public RepositoryUnitTest()
        {
            _fixture = new Fixture();

            var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(dbContextOptions);
            _context.Database.EnsureCreated();
        }

        /// <summary>
        ///   <para>
        ///     <br />
        ///   </para>
        ///   <para>Gets all character check character list.
        /// </para>
        /// </summary>
        [Fact]
        public async Task GetAllCharacter_CheckCharacterList()
        {
            //Arrange
            var newListCharacter = _fixture.CreateMany<Character>(5).ToList();
            _context.Characters.AddRange(newListCharacter);
            await _context.SaveChangesAsync();

            var _repository = new CharacterRepository(_context);

            //Action
            var getAllCharacter = await _repository.GetAllCharacter();

            //Assert
            Assert.Equal(newListCharacter.Count, getAllCharacter.Count);
        }

        /// <summary>Gets all character check without value.</summary>
        [Fact]
        public async Task GetAllCharacter_CheckWithoutValue()
        {
            //Arrange
            var _repository = new CharacterRepository(_context);

            //Action
            var getAllCharacter = await _repository.GetAllCharacter();

            //Assert
            Assert.False(getAllCharacter.Count > 0);
            Assert.Empty(getAllCharacter);
        }

        /// <summary>Creates the character check if have value.</summary>
        [Fact]
        public async Task CreateCharacter_CheckIfHaveValue()
        {
            //Arrange
            var newListCharacter = _fixture.CreateMany<Character>(5).ToList();
            _context.Characters.AddRange(newListCharacter);
            await _context.SaveChangesAsync();

            var newCharacter = _fixture.Create<CreateCharacterRequest>();

            var _repository = new CharacterRepository(_context);

            //Action
            var createCharacter = await _repository.CreateCharacter(newCharacter);

            //Assert
            var expectedValue = newListCharacter.Count + 1;
            var realValue = _context.Characters.ToList().Count;

            Assert.Equal(expectedValue, realValue);
        }

        /// <summary>Creates the character check without value.</summary>
        [Fact]
        public async Task CreateCharacter_CheckWithoutValue()
        {
            //Arrange
            var newListCharacter = _fixture.CreateMany<Character>(5).ToList();
            _context.Characters.AddRange(newListCharacter);
            await _context.SaveChangesAsync();

            CreateCharacterRequest newCharacter = null;

            var _repository = new CharacterRepository(_context);

            //Action
            var createCharacter = await _repository.CreateCharacter(newCharacter);

            //Assert
            var expectedValue = newListCharacter.Count + 1;
            var realValue = _context.Characters.ToList().Count;

            Assert.NotEqual(expectedValue, realValue);
        }
    }
}