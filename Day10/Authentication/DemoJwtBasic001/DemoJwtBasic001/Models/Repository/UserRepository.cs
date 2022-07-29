using DemoJwtBasic001.Models.Data;
using DemoJwtBasic001.Models.Interface;
using DemoJwtBasic001.Models.Model;

namespace DemoJwtBasic001.Models.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public User CreateUser(User user)
        {
            _context.Users.Add(user);
            user.Id = _context.SaveChanges();
            return user;
        }

        public User GetUserByEmail(string email)
        {
            User userFind = _context.Users.FirstOrDefault(u => u.Email == email);
            return userFind;
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
