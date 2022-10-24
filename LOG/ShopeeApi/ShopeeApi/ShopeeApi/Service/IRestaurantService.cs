using ShopeeApi.Dtos;
using ShopeeApi.Repository;

namespace ShopeeApi.Service
{
    public interface IRestaurantService
    {
        Task<ServiceResponse<ResponseGetRestaurant>> CreateRestaurant(RequestAddRestaurant request);

        Task<ServiceResponse<IEnumerable<ResponseGetRestaurant>>> GetAllRestaurant();

        Task<ServiceResponse<ResponseGetRestaurant>> GetRestaurantById(int ResId);

        Task<ServiceResponse<ResponseGetRestaurantWithFoodTag>> GetRestaurantByIdWithTagAndFood(int ResId);

        Task<ServiceResponse<ResponseGetRestaurant>> UpdateRestaurant(int ResId, RequestEditRestaurant request);

        Task<ServiceResponse<string>> DeleteRestaurant(int ResId);

        Task<ServiceResponse<IEnumerable<ResponseGetRestaurantWithCategoryTag>>> GetAllRestaurantWithTag();

        Task<ServiceResponse<IEnumerable<ResponseGetRestaurantWithFood>>> GetAllRestaurantWithFood();

        Task<ServiceResponse<IEnumerable<ResponseGetRestaurantWithFoodTag>>> GetAllRestaurantWithFoodTag();
    }
}