using ShopeeApi.Dtos;

namespace ShopeeApi.Repository
{
    public interface IUserRepository
    {
        Task<ServiceResponse<ResponseUserRegister>> Register(RequestUserRegister request);

        Task<ServiceResponse<string>> AuthenLogin(RequestUserLogin request);

        Task<ServiceResponse<ResponseViewUser>> ViewUserInfo(string jwtToken);
    }
}