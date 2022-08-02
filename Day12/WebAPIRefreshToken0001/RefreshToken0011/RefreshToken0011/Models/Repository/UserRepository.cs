using RefreshToken0011.Models.DatabaseContext;
using RefreshToken0011.Models.Dto;
using RefreshToken0011.Models.Helpers;
using RefreshToken0011.Models.Interface;
using RefreshToken0011.Models.ModelDatabase;

namespace RefreshToken0011.Models.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly JwtTokenHelper _tokenHelper;

        public UserRepository(DataContext context, JwtTokenHelper tokenHelper)
        {
            _context = context;
            _tokenHelper = tokenHelper;
        }

        public User CreateUser(User user)
        {
            _context.Users.Add(user);
            user.Id = _context.SaveChanges();
            return user;
        }

        public List<User> GetAllUser()
        {
            return _context.Users.ToList();
        }

        public User GetUserByUserId(int id)
        {
            return _context.Users.Find(id);
        }

        public User GetUserByUserName(string userName)
        {
            User userFind = _context.Users.FirstOrDefault(u => u.UserName == userName);

            return userFind;
        }

    }
}
