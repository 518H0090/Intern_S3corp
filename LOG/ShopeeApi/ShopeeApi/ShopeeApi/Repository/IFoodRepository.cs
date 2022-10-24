using ShopeeApi.Models;

namespace ShopeeApi.Repository
{
    public interface IFoodRepository
    {
        Task<Food> CreateFood(Food request);
        Task<IEnumerable<Food>> GetAllFood();
        Task<IEnumerable<Food>> GetAllFoodInRestaurant(Food request);
        Task<Food> GetFoodById(Food request);
        Task<Food> GetFoodInRestaurantById(Food request);
        Task<bool> ExistsFoodInRestaurant(Food request);
        Task<bool> ExistsRestaurant(Food request);
        Task<bool> DeleteFood(Food request);
        Task<Food> UpdateFood(Food request);
    }
}
