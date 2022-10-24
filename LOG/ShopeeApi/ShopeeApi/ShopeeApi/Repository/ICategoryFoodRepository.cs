using ShopeeApi.Models;

namespace ShopeeApi.Repository
{
    public interface ICategoryFoodRepository
    {
        Task<RelationCategoryFood> ConnectFoodTag(RelationCategoryFood request);

        Task<bool> ExistsRelationFoodTag(int resId,RelationCategoryFood request);

        Task<bool> DeleteRelationFoodTag(RelationCategoryFood request);

        Task<IEnumerable<RelationCategoryFood>> GetAllRelation();

        Task<IEnumerable<Category>> GetAllCategorywithRestaurantIdCombineFood(int resId);

        Task<Category> GetCategorywithRestaurantIdCombineFood(int resId, int cateId);

        Task<IEnumerable<Food>> GetAllFoodwithRestaurantIdCombineCategory(int resId);

        Task<Food> GetFoodwithRestaurantIdCombineCategory(int resId, int foodId);
    }
}
