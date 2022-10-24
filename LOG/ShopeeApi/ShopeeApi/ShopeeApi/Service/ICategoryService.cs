using ShopeeApi.Dtos;
using ShopeeApi.Repository;

namespace ShopeeApi.Service
{
    public interface ICategoryService
    {
        Task<ServiceResponse<ResponseGetCategory>> CreateCategory(RequestAddCategory request);

        Task<ServiceResponse<IEnumerable<ResponseGetCategory>>> GetFullCategories();

        Task<ServiceResponse<IEnumerable<ResponseGetCategory>>> GetAllCategories(int ResId);

        Task<ServiceResponse<ResponseGetCategory>> GetCategoryById(RequestCategoryById request);

        Task<ServiceResponse<ResponseGetCategory>> GetCategoryByTag(RequestCategoryByTag request);

        Task<ServiceResponse<string>> DeleteCategory(RequestDeleteCategory request);

        Task<ServiceResponse<ResponseGetCategory>> UpdateCategory(RequestEditCategory request);
    }
}