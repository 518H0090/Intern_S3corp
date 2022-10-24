namespace ShopeeApi.Dtos
{
    public class RequestUpdateFood
    {
        public string FoodImageUrl { set; get; } = string.Empty;

        public string FoodTitle { set; get; } = string.Empty;

        public string FoodDescription { set; get; } = string.Empty;

        public int FoodPrice { set; get; }

        public float FoodPriceLess { set; get; }

        public int FoodId { set; get; }

        public int RestaurantId { set; get; }

    }
}
