using System.Text.Json.Serialization;

namespace ShopeeApi.Models
{
    public class RelationCategoryFood
    {
        [JsonIgnore]
        public Category? Category { set; get; }
        public int CategoryId { get; set; }

        public Food? Food { set; get; }
        public int FoodId { set; get; }
    }
}
