using RefreshTokenWebApi.Models.DatabaseContext;
using RefreshTokenWebApi.Models.DatabaseModels;
using RefreshTokenWebApi.Models.DtoRespository.Request;
using RefreshTokenWebApi.Models.DtoRespository.Response;

namespace RefreshTokenWebApi.Models.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public ResponseUserLogin CreateUser(UserCreate request)
        {
            if (request.UserName == null || request.Password == null)
            {
                return null;
            }

            User createUser = new User
            {
                UserName = request.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            var createdUser = _context.Users.Add(createUser);
            _context.SaveChanges();

            ResponseUserLogin userResponse = new ResponseUserLogin
            {
                UserName = createdUser.Entity.UserName
            };

            return userResponse;
        }

        public ResponseFindUser FindUser(UserLogin request)
        {
            var findUser = _context.Users.Where(u => u.UserName == request.UserName).FirstOrDefault();

            if (findUser == null)
            {
                return null;
            }

            if(!BCrypt.Net.BCrypt.Verify(request.Password, findUser.Password))
            {
                return null;
            }

            ResponseFindUser userFind = new ResponseFindUser
            {
               UserName = findUser.UserName,
               Password = findUser.Password
            };

            return userFind;
        }
    }
}
