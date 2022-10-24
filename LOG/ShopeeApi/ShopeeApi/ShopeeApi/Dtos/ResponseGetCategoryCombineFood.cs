using ShopeeApi.Models;

namespace ShopeeApi.Dtos
{
    public class ResponseGetCategoryCombineFood
    {
        public string CategoryName { set; get; } = string.Empty;

        public string CategoryTag { set; get; } = string.Empty;

        public int RestaurantId { get; set; }

        public IEnumerable<ResponseGetFood> Foods { set; get; }
    }
}
