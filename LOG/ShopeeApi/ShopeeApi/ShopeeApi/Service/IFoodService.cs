using ShopeeApi.Dtos;
using ShopeeApi.Repository;

namespace ShopeeApi.Service
{
    public interface IFoodService
    {
        Task<ServiceResponse<ResponseGetFood>> CreateFood(RequestAddFood request);
        Task<ServiceResponse<IEnumerable<ResponseGetFood>>> GetAllFood();
        Task<ServiceResponse<IEnumerable<ResponseGetFood>>> GetAllFoodInRestaurant(int resId);
        Task<ServiceResponse<ResponseGetFood>> GetFoodById(int foodId);
        Task<ServiceResponse<ResponseGetFood>> GetFoodInRestaurantById(RequestFoodContainRestaurant request);
        Task<ServiceResponse<string>> DeleteFood(RequestFoodContainRestaurant request);
        Task<ServiceResponse<ResponseGetFood>> UpdateFood(RequestUpdateFood request);
    }
}
