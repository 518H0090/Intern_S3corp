using DemoJwtBasic001.Models.Model;

namespace DemoJwtBasic001.Models.Interface
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        User GetUserByEmail(string email);
        User GetUserById(int id);
    }
}
