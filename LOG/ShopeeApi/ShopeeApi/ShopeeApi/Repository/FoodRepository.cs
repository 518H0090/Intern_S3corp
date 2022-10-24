using Microsoft.EntityFrameworkCore;
using ShopeeApi.Datas;
using ShopeeApi.Models;

namespace ShopeeApi.Repository
{
    public class FoodRepository : IFoodRepository
    {
        private readonly DataContext _context;

        public FoodRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Food> CreateFood(Food request)
        {
            var newFood = await _context.Foods.AddAsync(request);
            await _context.SaveChangesAsync();

            return newFood.Entity;
        }

        public async Task<bool> DeleteFood(Food request)
        {
            var findFood = await _context.Foods
                .FirstOrDefaultAsync(x => x.FoodId == request.FoodId && x.RestaurantId == request.RestaurantId);

            if (findFood == null)
            {
                return false;
            }

            _context.Foods.Remove(findFood);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistsFoodInRestaurant(Food request)
        {
            var findFoodInRes = await _context.Foods
                .FirstOrDefaultAsync(x => x.FoodTitle == request.FoodTitle && x.RestaurantId == request.RestaurantId);

            if (findFoodInRes == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ExistsRestaurant(Food request)
        {
            var findRestaurant = await _context.Restaurants.FirstOrDefaultAsync(x => x.RsId == request.RestaurantId);

            if (findRestaurant == null)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<Food>> GetAllFood()
        {
            return await _context.Foods.ToListAsync();
        }

        public async Task<IEnumerable<Food>> GetAllFoodInRestaurant(Food request)
        {
            return await _context.Foods.Where(f => f.RestaurantId == request.RestaurantId).ToListAsync();
        }

        public async Task<Food> GetFoodById(Food request)
        {
            return await _context.Foods.FirstOrDefaultAsync(f => f.FoodId == request.FoodId);
        }

        public async Task<Food> GetFoodInRestaurantById(Food request)
        {
            return await _context.Foods.FirstOrDefaultAsync(f => f.FoodId == request.FoodId && f.RestaurantId == request.RestaurantId);
        }

        public async Task<Food> UpdateFood(Food request)
        {
            var findFood = await _context.Foods
                .FirstOrDefaultAsync(x => x.FoodId == request.FoodId && x.RestaurantId == request.RestaurantId);

            if (findFood == null)
            {
                return null;
            }

            findFood.FoodImageUrl = request.FoodImageUrl;
            findFood.FoodTitle = request.FoodTitle;
            findFood.FoodDescription = request.FoodDescription;
            findFood.FoodPrice = request.FoodPrice;
            findFood.FoodPriceLess = request.FoodPriceLess;

            var updatedFood = _context.Foods.Update(findFood);
            await _context.SaveChangesAsync();

            return updatedFood.Entity;
        }
    }
}


