using RefreshTokenJwtAuthentication.Models.DatabaseContext;
using RefreshTokenJwtAuthentication.Models.DatabaseModels;

namespace RefreshTokenJwtAuthentication.Models.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public User CreateUser(User user)
        {
            var createdUser = _context.Users.Add(user);
            _context.SaveChanges();
            return createdUser.Entity;
        }

        public List<User> GetAllUser()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }

        public User GetUserByName(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == userName);
        }
    }
}
