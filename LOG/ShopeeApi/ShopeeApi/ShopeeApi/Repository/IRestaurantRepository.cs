using ShopeeApi.Models;

namespace ShopeeApi.Repository
{
    public interface IRestaurantRepository
    {
        Task<Restaurant> CreateRestaurant(Restaurant request);

        Task<IEnumerable<Restaurant>> GetAllRestaurant();

        Task<Restaurant> GetRestaurantById(int ResId);

        Task<Restaurant> GetRestaurantByIdWithTagAndFood(int ResId);

        Task<bool> ExistRestaurant(string RsTitle);

        Task<Restaurant> UpdateRestaurant(int ResId, Restaurant request);

        Task<bool> DeleteRestaurant(int ResId);

        Task<IEnumerable<Restaurant>> GetAllRestaurantWithCategoryTag();

        Task<IEnumerable<Restaurant>> GetAllRestaurantWithFood();

        Task<IEnumerable<Restaurant>> GetAllRestaurantWithCategoryTagAndFood();
    }
}