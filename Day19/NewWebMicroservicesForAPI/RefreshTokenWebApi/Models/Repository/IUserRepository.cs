using RefreshTokenWebApi.Models.DtoRespository.Request;
using RefreshTokenWebApi.Models.DtoRespository.Response;

namespace RefreshTokenWebApi.Models.Repository
{
    public interface IUserRepository
    {
        ResponseUserLogin CreateUser(UserCreate request);
        ResponseFindUser FindUser(UserLogin request);
    }
}
