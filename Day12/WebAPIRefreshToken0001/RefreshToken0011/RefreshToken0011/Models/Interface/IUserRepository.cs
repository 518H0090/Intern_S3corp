using RefreshToken0011.Models.DatabaseContext;
using RefreshToken0011.Models.Dto;

namespace RefreshToken0011.Models.Interface
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        List<User> GetAllUser();
        User GetUserByUserName(string userName);
        User GetUserByUserId(int id);
    }
}
