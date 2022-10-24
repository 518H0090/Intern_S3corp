using Microsoft.EntityFrameworkCore;
using ShopeeApi.Datas;
using ShopeeApi.Models;

namespace ShopeeApi.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategory(Category request)
        {
            request.Restaurant = await _context.Restaurants.FindAsync(request.RestaurantId);

            var newCategory = await _context.Categories.AddAsync(request);
            await _context.SaveChangesAsync();

            return newCategory.Entity;
        }

        public async Task<bool> DeleteCategory(int ResId, int CateId)
        {
            var findCategory = await GetCategoryById(ResId, CateId);

            if (findCategory == null)
            {
                return false;
            }

            _context.Categories.Remove(findCategory);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistsCategoryTag(int ResId, string CateTag)
        {
            var checkTagForRestaurant = await GetCategoryByTag(ResId, CateTag);

            if (checkTagForRestaurant == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ExistsRestaurantOrNot(int ResId)
        {
            var findRestaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.RsId == ResId);

            if (findRestaurant == null)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<Category>> GetAllCategories(int ResId)
        {
            return await _context.Categories.Where(x => x.RestaurantId == ResId).ToListAsync();
        }

        public async Task<Category> GetCategoryById(int ResId, int CateId)
        {
            return await _context.Categories.Where(x => x.RestaurantId == ResId && x.CategoryId == CateId).FirstOrDefaultAsync();
        }

        public async Task<Category> GetCategoryByTag(int ResId, string CateTag)
        {
            return await _context.Categories.Where(x => x.RestaurantId == ResId && x.CategoryTag == CateTag).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Category>> GetFullCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> UpdateCategory(Category request)
        {
            var findCategory = await GetCategoryById(request.RestaurantId, request.CategoryId);

            if (findCategory == null)
            {
                return null;
            }

            findCategory.CategoryName = request.CategoryName;
            findCategory.CategoryTag = request.CategoryTag;

            var updateCategory = _context.Categories.Update(findCategory);
            await _context.SaveChangesAsync();

            return updateCategory.Entity;
        }
    }
}