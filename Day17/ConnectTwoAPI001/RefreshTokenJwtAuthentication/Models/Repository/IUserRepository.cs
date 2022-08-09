using RefreshTokenJwtAuthentication.Models.DatabaseModels;

namespace RefreshTokenJwtAuthentication.Models.Repository
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        List<User> GetAllUser();
        User GetUserByName(string userName);
        User GetUserById(int userId);
    }
}
