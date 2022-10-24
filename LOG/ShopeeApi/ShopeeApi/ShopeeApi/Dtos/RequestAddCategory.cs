namespace ShopeeApi.Dtos
{
    public class RequestAddCategory
    {
        public string CategoryName { set; get; } = string.Empty;

        public string CategoryTag { set; get; } = string.Empty;

        public int RestaurantId { get; set; }
    }
}