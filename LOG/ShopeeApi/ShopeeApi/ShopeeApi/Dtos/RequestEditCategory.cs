namespace ShopeeApi.Dtos
{
    public class RequestEditCategory
    {
        public int CategoryId { get; set; }

        public int RestaurantId { get; set; }

        public string CategoryName { set; get; } = string.Empty;

        public string CategoryTag { set; get; } = string.Empty;
    }
}