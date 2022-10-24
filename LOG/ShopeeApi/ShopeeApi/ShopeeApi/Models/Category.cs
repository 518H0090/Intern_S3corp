using System.Text.Json.Serialization;

namespace ShopeeApi.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { set; get; } = string.Empty;

        public string CategoryTag { set; get; } = string.Empty;

        public Restaurant Restaurant { set; get; }
        public int RestaurantId { get; set; }

        public IEnumerable<RelationCategoryFood> RelationCategoryFoods { get; set; }

        public IEnumerable<Food> Foods { set; get; }
    }
}