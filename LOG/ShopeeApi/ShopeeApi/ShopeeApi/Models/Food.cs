using System.Text.Json.Serialization;

namespace ShopeeApi.Models
{
    public class Food
    {
        public int FoodId { set; get; }

        public string FoodImageUrl { set; get; } = string.Empty;

        public string FoodTitle { set; get; } = string.Empty;   

        public string FoodDescription { set; get; } = string.Empty;

        public int FoodPrice { set; get; }

        public float FoodPriceLess { set; get; }

        public Restaurant Restaurant { set; get; }
        public int RestaurantId { set; get; }

        public IEnumerable<RelationCategoryFood> RelationCategoryFoods { get; set; }

        public IEnumerable<Category> Categories { set; get; }
    }
}