using Microsoft.EntityFrameworkCore;
using ShopeeApi.Datas;
using ShopeeApi.Models;

namespace ShopeeApi.Repository
{
    public class CategoryFoodRepository : ICategoryFoodRepository
    {
        private readonly DataContext _context;

        public CategoryFoodRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<RelationCategoryFood> ConnectFoodTag(RelationCategoryFood request)
        {
            var newRelationFoodTag = await _context.RelationCategoryFoods.AddAsync(request);
            await _context.SaveChangesAsync();

            return newRelationFoodTag.Entity;
        }

        public async Task<bool> DeleteRelationFoodTag(RelationCategoryFood request)
        {
            var findRelationFoodTag = await _context.RelationCategoryFoods
                .FirstOrDefaultAsync(x => x.FoodId == request.FoodId && x.CategoryId == request.CategoryId);

            if (findRelationFoodTag == null)
            {
                return false;
            }

            _context.RelationCategoryFoods.Remove(findRelationFoodTag);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistsRelationFoodTag(int resId, RelationCategoryFood request)
        {
            var checkRestaurant = await _context.Restaurants.FirstOrDefaultAsync(x => x.RsId == resId);
            var checkRestaurantCategory = await _context.Categories
                .Where(x => x.RestaurantId == resId && x.CategoryId == request.CategoryId)
                .FirstOrDefaultAsync();

            var checkRestaurantFood = await _context.Foods
                .Where(x => x.RestaurantId == resId && x.FoodId == request.FoodId)
                .FirstOrDefaultAsync();

            if (checkRestaurant == null || checkRestaurantCategory == null || checkRestaurantFood == null)
            {
                return false;
            }

            var checkRelation = await _context.RelationCategoryFoods
                .Where(x => x.CategoryId == request.CategoryId && x.FoodId == request.FoodId)
                .FirstOrDefaultAsync();

            if (checkRelation == null)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<RelationCategoryFood>> GetAllRelation()
        {
            return await _context.RelationCategoryFoods.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategorywithRestaurantIdCombineFood(int resId)
        {
            return await _context.Categories.Where(x => x.RestaurantId == resId)
               .Include(x => x.Foods)
               .ToListAsync();
        }

        public async Task<IEnumerable<Food>> GetAllFoodwithRestaurantIdCombineCategory(int resId)
        {
            return await _context.Foods.Where(x => x.RestaurantId == resId)
                .Include(x => x.Categories)
                .ToListAsync();
        }

        public async Task<Category> GetCategorywithRestaurantIdCombineFood(int resId, int cateId)
        {
            return await _context.Categories.Where(x => x.RestaurantId == resId) 
                .Include(x => x.Foods)
                .FirstOrDefaultAsync(x => x.CategoryId == cateId);
        }

        public async Task<Food> GetFoodwithRestaurantIdCombineCategory(int resId, int foodId)
        {
            return await _context.Foods
                .Where(x => x.RestaurantId == resId)
                .Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.FoodId == foodId);
        }
    }
}
