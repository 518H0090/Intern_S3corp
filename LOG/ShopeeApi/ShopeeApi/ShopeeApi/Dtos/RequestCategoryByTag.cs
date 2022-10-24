namespace ShopeeApi.Dtos
{
    public class RequestCategoryByTag
    {
        public string CategoryTag { set; get; } = string.Empty;

        public int RestaurantId { get; set; }
    }
}