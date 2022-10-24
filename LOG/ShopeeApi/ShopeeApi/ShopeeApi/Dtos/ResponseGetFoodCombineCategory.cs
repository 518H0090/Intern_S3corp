using ShopeeApi.Models;

namespace ShopeeApi.Dtos
{
    public class ResponseGetFoodCombineCategory
    {
        public string FoodImageUrl { set; get; } = string.Empty;

        public string FoodTitle { set; get; } = string.Empty;

        public string FoodDescription { set; get; } = string.Empty;

        public int FoodPrice { set; get; }

        public float FoodPriceLess { set; get; }

        public int RestaurantId { set; get; }

        public IEnumerable<ResponseGetCategory> Categories { set; get; }
    }
}
