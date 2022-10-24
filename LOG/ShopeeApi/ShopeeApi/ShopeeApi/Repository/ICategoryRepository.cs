using ShopeeApi.Models;

namespace ShopeeApi.Repository
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategory(Category request);

        Task<IEnumerable<Category>> GetFullCategories();

        Task<IEnumerable<Category>> GetAllCategories(int ResId);

        Task<Category> GetCategoryById(int ResId, int CateId);

        Task<Category> GetCategoryByTag(int ResId, string CateTag);

        Task<bool> ExistsCategoryTag(int ResId, string CateTag);

        Task<bool> DeleteCategory(int ResId, int CateId);

        Task<Category> UpdateCategory(Category request);

        Task<bool> ExistsRestaurantOrNot(int ResId);
    }
}